using bugeto_stor.common.dto;

namespace bugeto_stor.application.services.users.command.rgegisteruser
{
    internal class ResultDto<T> : resultdto<resultrgegisteruserdto>
    {
        public object Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}