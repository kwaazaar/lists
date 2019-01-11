using System;

namespace list.Models
{
    public enum QuestionType : int
    {
        Undefined = 0,
        Text = 1,
        Numeric = 2,
        Bool = 3
    }

    public class ListItemValue
    {
        public ListItemValue(QuestionType questionType, object theValue)
        {
            switch (questionType)
            {
                case QuestionType.Text:
                    ValueText = (string)theValue;
                    break;
                case QuestionType.Numeric:
                    ValueNumeric = (decimal?)theValue;
                    break;
                case QuestionType.Bool:
                    ValueBool = (bool?)theValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(questionType), questionType.ToString());
            }
        }

        public string ValueText { get;  }
        public decimal? ValueNumeric { get;  }
        public bool? ValueBool { get;  }

    }
}