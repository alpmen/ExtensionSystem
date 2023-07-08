using Newtonsoft.Json;

namespace MyMessageApp.Core.Models
{
    public class ErrorResultModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Key { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
