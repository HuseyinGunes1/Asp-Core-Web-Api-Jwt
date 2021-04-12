using System;
using System.Collections.Generic;
using System.Text;

namespace JWT.SHARED.Dtos
{
    public class ErrorDto
    {
        public List<string> Hata { get; private set; }
        public bool IsShow { get; private set; }
        public ErrorDto()
        {
            Hata = new List<string>();
        }
        public ErrorDto(string Error,bool isShow)
        {
            Hata.Add(Error);
            isShow = true;
        }
        public ErrorDto(List<string> error, bool isShow)
        {
            Hata = Hata;
            isShow = true;
        }
    }
}
