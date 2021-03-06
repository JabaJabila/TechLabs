# Лабораторные

## lab-1. Hello world
    
Задания на лабораторную работу:
    
1. Изучить механизм интеропа между языками, попробовать у себя вызывать C/C++ (Не C++/CLI) код (суммы чисел достаточно) из Java и C#. В отчёте описать логику работы, сложности и ограничения этих механизмов.
2. Написать немного кода на Scala **и** F# с использованием уникальных возможностей языка - Pipe operator, Discriminated Union, Computation expressions и т.д. . Вызвать написанный код из обычных соответствующих ООП языков (Java **и** С#) и посмотреть во что превращается написанный раннее код после декомпиляции в них. 
3. Написать алгоритм обхода графа (DFS и BFS) на языке Java, собрать в пакет и опубликовать (хоть в Maven, хоть в Gradle, не имеет значения). Использовать в другом проекте на Java/Scala этот пакет. Повторить это с C#/F#. В отчёте написать про алгоритм работы пакетных менеджеров, особенности их работы в C# и Java мирах.
4. Изучить инструменты для оценки производительности в C# и Java. Написать несколько алгоритмов сортировок (и взять стандартную) и запустить бенчмарки (в бенчмарках помимо времени выполнения проверить аллокации памяти). В отчёт написать про инструменты для бенчмаркинга, их особености, анализ результатов проверок.
5. Используя инструменты dotTrace, dotMemory, всё-что-угодно-хоть-windbg, проанализировать работу написанного кода для бекапов. Необходимо написать сценарий, когда в цикле будет выполняться много запусков, будут создаваться и удаляться точки. Проверить два сценария: с реальной работой с файловой системой и без неё. В отчёте неоходимо проанализировать полученные результаты, сделать вывод о написанном коде. Опционально: предложить варианты по модернизации или написать альтернативную имплементацию.
    
**Опционально**:
    
- Бенчмарк памяти для джавы
    
## lab-2. Codegen
  
Цель: используя инструмент Roslyn API написать программу, которая генерирует HTTP-клиент для сервера написанного на другом языке.
    
### Написание сервиса с которым будет выполняться работ
    
Написать HTTP-сервер, которые предоставляет несколько методов (в качестве примера, можно взять 2-3 лабораторные второго подпотока). Рекомендуемый язык - Java ввиду простоты поднятия и прочего. Можно использовать любой другой (лучше заранее согласовать). Примеры необходимого функционала:
    
1. GET, POST запросы
2. Запросы с аргументами в Query, в Body
3. Сложные модели с Response (не примитивы, хотя бы классы с полями)
4. Аргументы, которые являются коллекциями, респонсы, которые коллекции содержат
    
### Написание парсера
    
Написать упрощённый парсер (на C#) для этого сервера, чтобы можно было получить семантическую модель (можно использовать любые библиотеки для этого), а именно:
    
1. Описание методов из API - url, список аргументов, возвращаемое значение
2. Модели, которые используются в реквестах и респонсах
    
### Генерация клиента
    
Используя Roslyn API реализовать генерацию HTTP-клиента для данного сервера. Для API должны генерироваться все нужные модели, методы. Можно посмотреть на Swager codegen, примеры его работы и результат его генерации.
    
На защите нужно продемонстрировать написанные сервер и генератор, генерацию методов и моделей.
    
    
## lab-3. Roslyn analyzer
    
1. Склонировать шаблон https://github.com/is-tech-y24-1/AnalyzerTemplate и назвать {username}_Analyzer
2. В соответствии со своим вариантом выбрать два задания  во вкладке Варианты.
3. Для каждого задания необходимо реализовать анализатор и кодфиксер с использованием Roslyn.
4. Добавить тесты, которые бы отобразили работаспособность анализатора и кодфиксера. Учесть, что тестами для анализатора лучше покрывать как случаи срабатывания, так и случаи, когда не должно срабатывать.
    
    
- Для проверки на null использовать конструкции is null и is not null вместо == null и != null
- В сигнатурах свойств и публичных методов использовать IReadOnlyCollection<T> вместо List<T> и Array<T>
    
## lab-4. Perf tips

Цель: ознакомиться с инструментами и подходами, которые позволяют минимизировать аллокации памяти, расходы на GC. Написать решение, которое было бы оптимальным с точки зрения аллокаций и перфоманса.
    
### Подход к решению лабораторной работы
    
1. Ознакомьтесь с прикладной областью представленных вариантов, выберите какой-то из них.
2. Напишите минимальную реализацию поставленной задачи, опишите решение в первом пункте отчёта.
3. Соберите метрики: запустите бенчмарки тех частей алгоритмов, которые кажутся наиболее важными, сохраните результаты.
        
Протестируйте реализацию в dotTrace и dotMemory, сохраните полученные результаты (лучше их как-то подписать, т.к. их будет много).
        
Во второй пункт отчёта вставьте полученные результаты, а также дополните анализом: опишите проблемы, которые вы можете видеть на трейсах, предполагаемые причины и возможные фиксы.
        
4. Возьмите один из предложенных вариантов исправлений, которые были получены в пункте 3 и
- реализуйте его,
- соберите повторно метрики,
- сравните.
5. Повторите действия из пункта 4. Результаты (как успешные, так и не очень), добавьте в третий пункт отчёта.
    
### Вариант 1. Генетический алгоритм
    
Требуется реализовать алгоритм, который по заданным ограничениям будет подбирать оптимальное решение задачи, используя подходы, построенные на генетических алгоритмах.
