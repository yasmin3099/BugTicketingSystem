using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BUGSystem.BL;

public class GeneralResult
{
    public bool Success { get; set; } = true;
    public bool IsSuccess { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public List<ResultError>? Errors { get; set; } = null;

    public ResultError[] Error { get; set; } = [];
}
public class GeneralResult<T> : GeneralResult
{
    public T? Data { get; set; }
    public static GeneralResult<T> Success(T data) => new()
    {
        IsSuccess = true,
        Data = data
    };

    public static new GeneralResult<T> Failure(string message, string code = "GENERAL_ERROR") => new()
    {
        IsSuccess = false,
        Error = [new ResultError { Code = code, Message = message }]
    };

    public static new GeneralResult<T> Failure(params ResultError[] errors) => new()
    {
        IsSuccess = false,
        Error = errors

    };
    }
public class ResultError
{
    public string Message { get; set; } = string.Empty;
    public string? Code { get; set; } = null;
}
