using IJunior.TypedScenes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using Extensions;
using UnityEngine.Events;

public class LvlMenu : Menu
{
    [SerializeField] private LvlVisulizer _visualizerPrefab;

    [SerializeField] private Transform _container;
    [SerializeField] private LvlList _lvlList;

    private List<ReadOnlyLvlInfo> _finsihedLvls = new List<ReadOnlyLvlInfo>();

    public ReadOnlyLvlList Lvls => _lvlList;

    private bool _isInited = false;

    public event UnityAction OnGameLoading;

    public void Init(ReadOnlyLvlList lvlList = null)
    {
        if (lvlList != null)
            _lvlList = new LvlList(lvlList.InfosEnumrator);

        _lvlList.SetInfosIndexByOrder();

        FinishLvls(_finsihedLvls);

        Visualize(_lvlList.InfosEnumrator);

        _isInited = true;
    }

    public void AddFinishedLvl(ReadOnlyLvlInfo finishedLvl)
    {
        if (_isInited)
            FinishLvl(finishedLvl);
        else
            _finsihedLvls.Add(finishedLvl);
    }

    private void FinishLvls(IEnumerable<ReadOnlyLvlInfo> lvlsToFinish)
    {
        foreach(var lvl in lvlsToFinish)
        {
            FinishLvl(lvl);
        }
    }

    private void FinishLvl(ReadOnlyLvlInfo lvlToFinish)
    {
        _lvlList.FinishLvl(lvlToFinish);
    }

    private void Visualize(IEnumerable<ReadOnlyLvlInfo> infos)
    {
        foreach (var info in infos)
            Visualize(info);
    }

    private void Visualize(ReadOnlyLvlInfo info)
    {
        LvlVisulizer newVisualize = Instantiate(_visualizerPrefab, _container);

        newVisualize.Visualize(info);

        newVisualize.OnSellected += GoToGame;
    }

    private void GoToGame(ReadOnlyLvlInfo info)
    {
        if (IsPrewiusLvlComplited(info) == false)
            return;

        OnGameLoading?.Invoke();

        GameScene.LoadAsync(new GameSceneLoadArguments(info));
    }

    private bool IsPrewiusLvlComplited(ReadOnlyLvlInfo info)
    {
        const int _prewiusLvlIndex = 1;

        var lvlIndex = (int)info.Index;

        if (_lvlList.Count <= lvlIndex)
            return false;

        if (lvlIndex - _prewiusLvlIndex >= 0)
            return _lvlList[lvlIndex - _prewiusLvlIndex].IsComplited;

        return true;
    }

    [System.Serializable]
    public class LvlList : ReadOnlyLvlList
    {
        public ReadOnlyLvlInfo this[int index]
        {
            get
            {
                if (Infos.OutOfRange(index))
                    throw new ArgumentOutOfRangeException();

                return Infos[index];
            }
        }

        public LvlList(IEnumerable<ReadOnlyLvlInfo> infos) : base(infos)
        { }

        public void FinishLvl(ReadOnlyLvlInfo lvlToFinish)
        {
            if (Infos.Count <= lvlToFinish.Index)
                throw new ArgumentOutOfRangeException();

            Infos[(int)lvlToFinish.Index].Complite();
        }
    }

    [System.Serializable]
    public class ReadOnlyLvlList
    {
        [SerializeField] private List<LvlInfo> _infos;

        protected IReadOnlyList<LvlInfo> Infos => _infos;

        public IEnumerable<ReadOnlyLvlInfo> InfosEnumrator => _infos;

        public int Count => _infos.Count;

        public ReadOnlyLvlList(IEnumerable<ReadOnlyLvlInfo> infos)
        {
            _infos = infos.Select(info => info.Clone()).ToList();

            SetInfosIndexByOrder();
        }

        public void SetInfosIndexByOrder()
        {
            for (uint i = 0; i < _infos.Count; i++)
            {
                _infos[(int)i].Index = i;
            }
        }
    }

    [System.Serializable]
    public class LvlInfo : ReadOnlyLvlInfo
    {
        public new uint Index { set { _index = value; } }

        public LvlInfo(Sprite lable, string name, Texture2D gameMap, bool isComplited) : base(lable, name, gameMap, isComplited)
        { }

        public new void Complite()
        {
            base.Complite();
        }
    }

    [System.Serializable]
    public class ReadOnlyLvlInfo : IClonable<LvlInfo>
    {
        [SerializeField] private Sprite _lable;
        [SerializeField] private string _name;

        [SerializeField] private Texture2D _gameMap;
        [SerializeField] private Vector2Int _playerPosition;

        [SerializeField] private bool _isComplited;

        protected uint _index;

        public Sprite Lable => _lable;
        public string Name => _name;

        public Texture2D GameMap => _gameMap;
        public Vector2Int PlayerPosition => _playerPosition;

        public bool IsComplited => _isComplited;

        public uint Index => _index;

        public ReadOnlyLvlInfo(Sprite lable, string name, Texture2D gameMap, bool isComplited)
        {
            _lable = lable;
            _name = name;
            _gameMap = gameMap;
            _isComplited = isComplited;
        }

        protected void Complite()
        {
            _isComplited = true;
        }

        public LvlInfo Clone()
        {
            return new LvlInfo(_lable, _name, _gameMap, _isComplited);
        }
    }
}
