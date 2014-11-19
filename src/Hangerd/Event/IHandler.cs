namespace Hangerd.Event
{
	public interface IHandler<in T>
	{
		void Handle(T @event);
	}
}
