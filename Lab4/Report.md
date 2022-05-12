# Отчёт по лабораторной работе №4

## Подготовительный этап

Я реализовал генетический алгоритм, симулирующий эволюционные процессы одноклеточных организмов.
Есть игровое поле X на Y клеток, на нём появляется N существ. Также на карте повлется еда и яд. У кажого существа есть здоровье и набор генов. Когда существо съедает еду, хдоровье восполняется, когда съедает яд - умирает. Каждый ген представляет саюой объект, реализующий интерфейс IGene и выполняет какой-либо действие над существом с помощью метода  ```void Execute(Creature creature)``` и отнимает очки здоровья. Основные виды дейтсвий над существом:
- Шагнуть на 1 клетку в одну из сторон и перейти к выполнению следующего гена
- Съесть содержимое клетки на расстоянии 1 клетки от существа и перейти к выполнению следующего гена
- Превратить яд в еду и перейти к выполнению следующего гена
- Посмотреть на клетку рядом и в зависисмости от содержимого перейти к другому гену
- Безусловный переход к другой части генотипа

Итерацией считается выполнение 1 гена у всех живых существ. После каждой итерации количество еды и яда на карте восстанавливаются. Итерации происходят пока не остается M% от начальной популяции. Эти существа прожили больше всего итераций и выиграли в результате естественного отбора. Происходит процесс размножения. Если существа находятся близко друг к другу, то их потомки получают пополам рандомных генов от обоих родителей, иначе существо размножается самоделением. В процессе размножения генотипы потомков мутриуют (рандомно изменяеются какие-то гены). Поколение на этом заершается, Начинаются новые итерации.

Также было написано приложение на WPF, визулизирующее карту после каждой итерации. Обучаясь около 500 поколений я достиг увеличения выживаемости существ с 20-30 итераций, до 200+ итераций.

Ссылка на [реализацию алгоритма](https://github.com/is-tech-y24-1/JabaJabila/tree/main/Lab4): https://github.com/is-tech-y24-1/JabaJabila/tree/main/Lab4

---
## Проверка производительности

Проведём бэнчмарк работы 10 поколений для дальнейшего сравнения написанных оптимизаций.

|         Method | Generations |    Mean |    Error |   StdDev |      Gen 0 | Allocated |
|--------------- |------------ |--------:|---------:|---------:|-----------:|----------:|
| RunGenerations |          10 | 1.272 s | 0.0310 s | 0.0894 s | 11000.0000 |     49 MB |


Также заставим наш алгоритм отработать 100 поколений без графического вывода и посмотрим результы профилирования через dotTrace и dotMemory.


![trace before](pictures/1traceAllocations.jpg)

![memory1 before](pictures/1memory_graph.jpg)

![memory2 before](pictures/1memory_compare.jpg)

![memory3 before](pictures/1memory_creature.jpg)

Работа 100 поколений в цикле заняла 184 секунды и суммарно было аллоцировано 1189 Мб памяти на куче. При этом работа программы требовала в моменте не более 25 Мб оперативной памяти за всё время работы.

Анализ работы алгоритма показал несколько проблемных мест:

1. Узким горлышком оказалось свойство AliveCreatures класса Population, которое при каждом get обращении формировала List живых существ из списка всех сущностей на карте с помощью Linq запроса.

2. На последнем скриншоте видно, что List-ы аллоцируют много памяти во время увеличения capacity при добавлении в List нового объекта.

3. Алгоритм подразумевает существование конечного набора одинаковых генов, которые используется каждым существом, а в алгоритме при генерации генотипа существа создается новый объект гена, в результате чего создаётся много лишних объектов.

4. Создаётся много объектов Location, который представляет из себя точку с 2-мя координатами.

---

## Улучшения оптимизации

### Проблема №1


```cs
public class Population
{
    private readonly IPopulationConfiguration _configuration;
    private readonly Creature[] _creatures;

    public Population(
        IPopulationConfiguration populationConfiguration,
        ICreatureConfiguration creatureConfiguration,
        IGeneFactory geneFactory)
    {
        ...
    }
    
    public Population(
        IPopulationConfiguration populationConfiguration, 
        Creature[] creatures)
    {
        ...
    }

    public IReadOnlyCollection<Creature> AllCreatures => _creatures;
    
    public IReadOnlyCollection<Creature> AliveCreatures => _creatures.Where(c => c.IsAlive).ToList();

    public bool IsInBreedZone => AliveCreatures.Count <= _configuration.PopulationBreedZone;
}
```



```cs
public IReadOnlyCollection<Creature> AllCreatures => _creatures;

public bool IsInBreedZone => AllCreatures.Count(c => c.IsAlive) <= _configuration.PopulationBreedZone;
```

### Проблема №3

```cs
public class GeneCreator : IGeneFactory
{
    private static readonly Random Random;
    private static readonly int[] Codes
        = { 10,11,12,13,14,15,16,17,20,21,22,23,24,25,26,27,30,31,32,33,34,35,36,37,40,41,42,43,44,45,46,47,50,51,52,53,54,55,56,57 };
    
    static GeneCreator()
    {
        ...
    }
    
    public IGene CreateRandomGene()
    {
        ...
    }

    public IGene CreateGeneFromCode(int code)
    {
        return (code / 10) switch
        {
            1 => new WalkingGene(code % 10),
            2 => new EatingGene(code % 10),
            3 => new NeutralizingGene(code % 10),
            4 => new UnconditionalTransitionGene(code % 10),
            5 => new ConditionalTransitionGene(code % 10),
            _ => throw new GeneticAlgoException($"Gene with code {code} doesn't exist"),
        };
    }

    private static int GetRandomCode()
    {
        return  Codes[Random.Next(0, Codes.Length)];
    }
}
```

```cs
public class GenePool : IGeneFactory
{
    private static readonly Random Random;
    private static readonly int[] Codes
        = { 10,11,12,13,14,15,16,17,20,21,22,23,24,25,26,27,30,31,32,33,34,35,36,37,40,41,42,43,44,45,46,47,50,51,52,53,54,55,56,57 };

    private static readonly Dictionary<int, IGene> Genes;

    static GenePool()
    {
        Random = new Random((int) DateTime.Now.Ticks & 0x0000FFFF);

        Genes = new Dictionary<int, IGene>();
        foreach (var code in Codes)
        {
            Genes[code] = (code / 10) switch
            {
                1 => new WalkingGene(code % 10),
                2 => new EatingGene(code % 10),
                3 => new NeutralizingGene(code % 10),
                4 => new UnconditionalTransitionGene(code % 10),
                5 => new ConditionalTransitionGene(code % 10),
                _ => throw new GeneticAlgoException($"Gene with code {code} doesn't exist"),
            };
        }
    }
    
    public IGene CreateRandomGene()
    {
        return CreateGeneFromCode(GetRandomCode());
    }

    public IGene CreateGeneFromCode(int code)
    {
        return Genes[code];
    }

    private static int GetRandomCode()
    {
        return  Codes[Random.Next(0, Codes.Length)];
    }
}
```

### Проблема №4

4. readonly struct


### Ещё проблемы