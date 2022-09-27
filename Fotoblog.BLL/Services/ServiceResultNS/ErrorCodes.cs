namespace Fotoblog.BLL.Services.ServiceResultNS
{
    public enum ErrorCodes
    {
        // general
        GeneralError = 0,
        // tags
        TagAlreadyCreated = 10,
        TagNotExists = 11,
        // photo upload
        FileExtensionNotAllowed = 20,
        FileLengthZero = 21,
        CouldNotSaveTheFile =22,
        // photo delete
        PhotoNotExists = 30,

        // authentication
        BadCredentials = 40,
        AdminAlreadyRegistered = 41
    }
}
