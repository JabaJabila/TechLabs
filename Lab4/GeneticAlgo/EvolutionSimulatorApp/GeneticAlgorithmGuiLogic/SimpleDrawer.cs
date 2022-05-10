using System;
using System.Collections.Generic;
using System.Windows.Controls;
using AlgorithmLogic.Map.MapEntities;

namespace EvolutionSimulatorApp.GeneticAlgorithmGuiLogic;

public class SimpleDrawer : IDrawer
{
    private readonly uint _cellSize;
    private readonly Canvas _canvas;
    private readonly int _waitTime;

    public SimpleDrawer(uint cellSize, Canvas canvas, int waitTime)
    {
        _cellSize = cellSize;
        _waitTime = waitTime;
        _canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
    }

    public void Draw(IReadOnlyCollection<IMapEntity> entities)
    {
        throw new NotImplementedException();
    }
}