using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebMS;

public static class Extensions
{
    public static bool IsAjax(this HttpRequest request)
    {
        return request.Headers.XRequestedWith == "XMLHttpRequest";
    }

    public static string GoBack(this HttpRequest request)
    {
        return request.Headers.Referer.ToString();
    }

    public static bool IsValid(this ModelStateDictionary ms, string key)
    {
        return ms.GetFieldValidationState(key) == ModelValidationState.Valid;
    }

}
