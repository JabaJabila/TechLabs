using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AlgorithmLogic.Configuration;
using AlgorithmLogic.Map.MapEntities;

namespace EvolutionSimulatorApp.GeneticAlgorithmGuiLogic;

public class SimpleDrawer : IDrawer
{
    private readonly IMapConfiguration _configuration;
    private readonly int _waitTime;
    private readonly WriteableBitmap _writableBitmap;
    private readonly byte[,,] _pixels;

    public SimpleDrawer(Image image, int waitTime, IMapConfiguration mapConfiguration)
    {
        _waitTime = waitTime;
        _configuration = mapConfiguration ?? throw new ArgumentNullException(nameof(mapConfiguration));
        var image1 = image ?? throw new ArgumentNullException(nameof(image));

        _writableBitmap = new WriteableBitmap(
            (int) mapConfiguration.MapWidth,
            (int) mapConfiguration.MapHeight,
            12,
            12,
            PixelFormats.Bgr32,
            null);

        image1.Source = _writableBitmap;
        _pixels = new byte[mapConfiguration.MapHeight, mapConfiguration.MapWidth, 4];
    }

    public void Draw(IReadOnlyCollection<IMapEntity> entities)
    {
        DrawEmptyFiled();
        
        foreach (var entity in entities)
        {
            switch (entity)
            {
                case Food:
                    DrawFood(entity);
                    break;
                case Poison:
                    DrawPoison(entity);
                    break;
                case CreatureEntity creature:
                    if (!creature.Creature.IsAlive) break;
                    DrawCreature(entity);
                    break;
            }
        }
        
        PrintPixels(_pixels);
    }

    private void DrawEmptyFiled()
    {
        for (var row = 0; row < _configuration.MapHeight; row++)
        for (var col = 0; col < _configuration.MapWidth; col++)
        {
            for (var i = 0; i < 3; i++)
                _pixels[row, col, i] = 0;
            _pixels[row, col, 3] = 255;
        }
    }

    private void DrawFood(IMapEntity entity)
    {
        _pixels[entity.Location.Y, entity.Location.X, 0] = 0;
        _pixels[entity.Location.Y, entity.Location.X, 1] = 255;
        _pixels[entity.Location.Y, entity.Location.X, 2] = 0;
    }
    
    private void DrawPoison(IMapEntity entity)
    {
        _pixels[entity.Location.Y, entity.Location.X, 0] = 0;
        _pixels[entity.Location.Y, entity.Location.X, 1] = 0;
        _pixels[entity.Location.Y, entity.Location.X, 2] = 255;
    }
    
    private void DrawCreature(IMapEntity entity)
    {
        _pixels[entity.Location.Y, entity.Location.X, 0] = 255;
        _pixels[entity.Location.Y, entity.Location.X, 1] = 255;
        _pixels[entity.Location.Y, entity.Location.X, 2] = 255;
    }
    
    private byte[] TransformTo1D(byte[,,] pixels)
    {
        var pixels1D = new byte[_configuration.MapHeight * _configuration.MapWidth * 4];

        var index = 0;
        for (var row = 0; row < _configuration.MapHeight; row++)
        for (var col = 0; col < _configuration.MapWidth; col++)
        for (var i = 0; i < 4; i++)
            pixels1D[index++] = pixels[row, col, i];

        return pixels1D;
    }

    private void PrintPixels(byte[,,] pixels)
    {
        var pixels1D = TransformTo1D(pixels);
        var rect = new Int32Rect(0, 0, (int) _configuration.MapWidth, (int) _configuration.MapHeight);
        var stride = (int) (4 * _configuration.MapWidth);

        Application.Current.Dispatcher.Invoke(() => _writableBitmap.WritePixels(rect, pixels1D, stride, 0));
    }
}