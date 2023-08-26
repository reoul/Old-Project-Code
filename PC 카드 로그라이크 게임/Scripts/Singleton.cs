﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour, ISingleton where T : MonoBehaviour
{
    // Destroy 여부 확인용
    private static bool _shuttingDown = false;
    private static object _lock = new object();
    
    private static T _inst;
    public static T Inst
    {
        get
        {
            // 게임 종료 시 Object 보다 싱글톤의 OnDestroy 가 먼저 실행 될 수도 있다. 
            // 해당 싱글톤을 gameObject.Ondestory() 에서는 사용하지 않거나 사용한다면 null 체크를 해주자
            if (_shuttingDown)
            {
                Debug.Log($"[Singleton] Instance '{typeof(T)}' already destroyed. Returning null.");
                return null;
            }

            lock (_lock) //Thread Safe
            {
                if (_inst == null)
                {
                    // 인스턴스 존재 여부 확인
                    _inst = (T) FindObjectOfType(typeof(T));

                    // 아직 생성되지 않았다면 인스턴스 생성
                    if (_inst == null)
                    {
                        // 새로운 게임오브젝트를 만들어서 싱글톤 Attach
                        var singletonObject = new GameObject();
                        _inst = singletonObject.AddComponent<T>();

                        GameObject managerObject = GetObjFromResource();
                        if (managerObject != null)
                        {
                            singletonObject.GetComponent<ISingleton>().DestroySingleton();
                            singletonObject = Instantiate(managerObject);
                            _inst = singletonObject.GetComponent<T>();
                        }
                        
                        singletonObject.name = $"{typeof(T)} (Singleton)";
                        singletonObject.tag = "Manager";

                        // Make instance persistent.
                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return _inst;
            }
        }
    }

    private void OnApplicationQuit()
    {
        _shuttingDown = true;
    }

    private void OnDestroy()
    {
        _shuttingDown = true;
    }

    /// <summary>
    /// 해당 클래스 싱글톤이 존재하는지 체크해서
    /// 존재 한다면 오브젝트 제거
    /// </summary>
    protected void CheckExistInstanceAndDestroy(T inst)
    { 
        if ((Inst != null) && (Inst != inst))
        {
            DestroySingleton();
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 싱글톤 오브젝트 삭제
    /// </summary>
    public void DestroySingleton()
    {
        _inst = null;
        DestroyImmediate(gameObject);
        _shuttingDown = false;
    }

    /// <summary>
    /// 프리팹화된 매니저 오브젝트를 Resource 폴더에서 가져온다.
    /// </summary>
    /// <returns></returns>
    private static GameObject GetObjFromResource()
    {
        return Resources.Load($"Prefabs/{typeof(T)}") as GameObject;
    }
}
