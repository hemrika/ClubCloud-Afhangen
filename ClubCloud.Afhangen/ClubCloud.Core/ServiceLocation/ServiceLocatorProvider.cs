
namespace ClubCloud.Core.ServiceLocation
{
    /// <summary>
    /// This delegate type is used to provide a method that will
    /// return the current container. Used with the <see cref="T:ClubCloud.Core.ServiceLocation.ServiceLocator" />
    /// static accessor class.
    /// </summary>
    /// <returns>An <see cref="T:ClubCloud.Core.ServiceLocation.IServiceLocator" />.</returns>
    public delegate IServiceLocator ServiceLocatorProvider();
}
