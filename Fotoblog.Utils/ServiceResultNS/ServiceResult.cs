namespace Fotoblog.Utils.ServiceResultNS
{
    public class ServiceResult
    {
        public ErrorCodes? ErrorCode { get; set; }
        public bool Status { get; set; }

        private ServiceResult(bool status = false)
        {
            Status = status;
        }

        private ServiceResult(bool status = false, ErrorCodes code = ErrorCodes.GeneralError)
        {
            Status = status;
            ErrorCode = code;
        }

        public static ServiceResult Ok()
        {
            return new ServiceResult(true);
        }

        public static ServiceResult Fail(ErrorCodes code)
        {
            return new ServiceResult(code: code);
        }
    }

    public class ServiceResult<T>
    {
        public ErrorCodes? ErrorCode { get; set; }
        public bool Status { get; set; }
        public T ?Data { get; set; }

        private ServiceResult(
            T data,
            bool status = false
            )
        {
            Data = data;
            Status = status;
        }

        private ServiceResult(
            T data,
            ErrorCodes code
        )
        {
            Data = data;
            Status = false;
            ErrorCode = code;
        }

        private ServiceResult(ErrorCodes code)
        {
            Status = false;
            ErrorCode = code;
        }

        public static ServiceResult<T> Ok(T data)
        {
            return new ServiceResult<T>(data, status: true);
        }

        public static ServiceResult<T> Fail(T data, ErrorCodes code)
        {
            return new ServiceResult<T>(data, code);
        }

        public static ServiceResult<T> Fail(ErrorCodes code)
        {
            return new ServiceResult<T>(code);
        }
    }
}
