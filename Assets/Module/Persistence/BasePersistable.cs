using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public abstract class BasePersistable<T>: IInitializable, IDisposable
{
    [Inject]
    private SaveManager m_saveManager;
    public void Initialize()
    {
        m_saveManager.Load(this);                               
        OnLoad();
    }

    public abstract void OnLoad();

    public void Dispose()
    {
        BeforeSave();
        m_saveManager.Save(this);
    }

    public abstract void BeforeSave();
}
