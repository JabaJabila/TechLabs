using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using AlgorithmLogic.Configuration;
using AlgorithmLogic.Map.MapEntities;

namespace EvolutionSimulatorApp.GeneticAlgorithmGuiLogic;

public class SimpleDrawer : IDrawer
{
    private readonly uint _cellSize;
    private readonly IMapConfiguration _configuration;
    private readonly Canvas _canvas;
    private readonly int _waitTime;

    public SimpleDrawer(uint cellSize, Canvas canvas, int waitTime, IMapConfiguration mapConfiguration)
    {
        _cellSize = cellSize;
        _waitTime = waitTime;
        _configuration = mapConfiguration ?? throw new ArgumentNullException(nameof(mapConfiguration));
        _canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
    }

    public void Draw(IReadOnlyCollection<IMapEntity> entities)
    {
        DrawEmptyFiled();
        
        foreach (var entity in entities)
        {
            switch (entity)
            {
                case Food food:
                    DrawFood(food);
                    break;
                case Poison poison:
                    DrawPoison(poison);
                    break;
                case CreatureEntity creature:
                    DrawCreature(creature);
                    break;
            }
        }
    }

    private void DrawEmptyFiled()
    {
        for (var i = 0; i < _configuration.MapWidth; i++)
        {
            for (var j = 0; j < _configuration.MapHeight; j++)
            {
                var rect = new Rectangle  
                {  
                    Width = _cellSize,  
                    Height = _cellSize,  
                    Fill = Brushes.Black,
                    Stroke = Brushes.Gray,
                };
                    
                _canvas.Children.Add(rect);  
                Canvas.SetTop(rect, j * _cellSize);  
                Canvas.SetLeft(rect, i * _cellSize); 
            }
        }
    }

    private void DrawFood(Food food)
    {
        var rect = new Rectangle  
        {  
            Width = _cellSize,  
            Height = _cellSize,  
            Fill = Brushes.Lime,
            Stroke = Brushes.Gray,
        };
                    
        _canvas.Children.Add(rect);  
        Canvas.SetTop(rect, food.Location.Y * _cellSize);  
        Canvas.SetLeft(rect, food.Location.X * _cellSize); 
    }
    
    private void DrawPoison(Poison poison)
    {
        var rect = new Rectangle  
        {  
            Width = _cellSize,  
            Height = _cellSize,  
            Fill = Brushes.Firebrick,
            Stroke = Brushes.Gray,
        };
                    
        _canvas.Children.Add(rect);  
        Canvas.SetTop(rect, poison.Location.Y * _cellSize);  
        Canvas.SetLeft(rect, poison.Location.X * _cellSize); 
    }
    
    private void DrawCreature(CreatureEntity creature)
    {
        var rect = new Rectangle  
        {  
            Width = _cellSize,  
            Height = _cellSize,  
            Fill = Brushes.Snow,
            Stroke = Brushes.Gray,
        };
                    
        _canvas.Children.Add(rect);  
        Canvas.SetTop(rect, creature.Location.Y * _cellSize);  
        Canvas.SetLeft(rect, creature.Location.X * _cellSize); 
    }
}