using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public class ActivityLogger
{
    private const int PageSize = 5;
    private int _additionalPagesShown = 0; 
    private readonly List<string> _fullLog = new();

    public ObservableCollection<string> VisibleLog { get; } = new();

    public void Log(string action)
    {
        
        _fullLog.Insert(0, $"{DateTime.Now:HH:mm:ss} - {action}");

        // Refresh the visible log to show latest entries
        RefreshVisibleLog();
    }

    public void ShowMore()
    {
        
        _additionalPagesShown++;
        RefreshVisibleLog();
    }

    private void RefreshVisibleLog()
    {
        VisibleLog.Clear();

       
        int totalItemsToShow = PageSize + (_additionalPagesShown * PageSize);
        int itemsToTake = Math.Min(totalItemsToShow, _fullLog.Count);

        // Add items from newest to oldest 
        for (int i = 0; i < itemsToTake; i++)
        {
            VisibleLog.Add(_fullLog[i]);
        }
    }

    
    public void ShowLatestOnly()
    {
        _additionalPagesShown = 0;
        RefreshVisibleLog();
    }

    
    public bool HasMoreLogs => _fullLog.Count > PageSize + (_additionalPagesShown * PageSize);
}