using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chamizo.Class
{
    public class FormatData
    {
        //Transforma o int de uma data em um valor para String. Ex: 1 = "01"
        public String FormatIntForDate(int value)
        {
            try
            {
                string StringValue = value.ToString();
                if (StringValue.Length == 1)
                    return "0" + StringValue;
                else
                    return StringValue;
            }
            catch (Exception ex)
            {
                return ("Erro: " + ex.Message);
            }
        }

        //Transforma o String de uma data em um valor para String. Ex: "1" = "01"
        public String FormatStringForDate(String value)
        {
            try
            {
                if (value.Length == 1)
                    return "0" + value;
                else
                    return value;

            }
            catch (Exception ex)
            {
                return ("Erro: " + ex.Message);
            }
        }

        //Remove os caracteres especiais e acentuados da String. Lista de a - z. Ex: "Código" = "Codigo". 
        public String FormatStringClear(String value)
        {
            try
            {
                String Pattern = @"(?i)[^0-9a-z\s]";
                String Replacement = "";
                Regex regex = new Regex(Pattern);
                return regex.Replace(value, Replacement);
            }
            catch (Exception ex)
            {
                return ("Erro: " + ex.Message);
            }
        }

        //Retorna dia mês e ano de um DateTime
        public String FormatDateForString(DateTime value)
        {
            try
            {
                return value.ToString("dd/MM/yyyy");
            }
            catch (Exception ex)
            {
                return ("Erro: " + ex.Message);
            }
        }

        //Recebe o valor e completa o lenght com o caractere pelo lado descrito, caso o length do valor seja maior, o valor será cortado pelo valor no length selecionado
        //value = valor, lenght = tamanho, character = caractere, side = lado. Se side = true o  caractere será inserido pela direita
        public String CompleteLenForString(String value, Int32 length, String character, Boolean side = true)
        {
            try
            {
                String result = value;
                if (value.Length < length)
                    if (side)
                        while (result.Length < length)
                            result = result + character;
                    else
                        while (result.Length < length)
                            result = character + result;
                else if (value.Length > length)
                    if (side)
                        result = result.Substring(0, length);
                    else
                        result = result.Substring(length);
                else
                    result = value;

                return result;
            }
            catch (Exception ex)
            {
                return ("Erro: " + ex.Message);
            }
        }

        //Checa se o valor esta entre o periodo colocado, incluindo ou não. Ex: 100 está entre 50 e 150
        public Boolean CheckBetweenValues(Int32 value, Int32 begin, Int32 end, Boolean including = true)
        {
            try
            {
                if (including)
                    if (value >= begin && value <= end)
                        return true;
                    else
                        return false;
                else
                    if (value > begin && value < end)
                        return true;
                    else
                        return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
