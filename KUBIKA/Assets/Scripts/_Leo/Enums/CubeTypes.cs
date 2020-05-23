public enum CubeTypes
{
    None = 0, //empty layer
    StaticCube, //full layer
    RotatorLocker, //full layer
    DeliveryCube, //full layer
    SwitchButton, //full layer
    RotatorRightTurner, //full layer
    RotatorLeftTurner, //full layer

    //less than 6 means full
    //bigger than 6 means it's moveable

    MoveableCube, //moveable layer
    
    BaseVictoryCube, //moveable layer
    ConcreteVictoryCube, //moveable layer
    BombVictoryCube, //moveable layer
    SwitchVictoryCube, //moveable layer

    ElevatorCube, //moveable layer
    ConcreteCube, //moveable layer
    BombCube, //moveable layer
    TimerCube, //moveable layer
    SwitchCube, //moveable & full layer

    ChaosBall, //moveable layer
}
