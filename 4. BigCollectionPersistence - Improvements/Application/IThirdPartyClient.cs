namespace Application;

public interface IThirdPartyClient
{
    Task CallThirdPartyAsync(string entry);
}