namespace Automata.Event
{
    /// <summary>
    /// Defines a delegate to use when a state related event occurs.
    /// </summary>
    /// <param name="sender">The triggerer of the event.</param>
    /// <param name="args">The arguments of this event.</param>
    public delegate void StateDelegate(object sender, StateEventArgs args);
}
