using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGSetGenerator
{
    public class CardForms
    {
        public static void InitCardForms()
        {
            Forms = new Dictionary<Label, CardForm>()
            {
                { Label.NONE, new CardForm("") },
                { Label.STANDARD, new CardForm("standard") },
                { Label.TOKEN, new CardForm("token") },
                { Label.TRANSFORM, new CardForm("transform") }
            };
        }

        public static Dictionary<Label, CardForm> Forms;

        public enum Label
        {
            NONE,

            STANDARD,
            TOKEN,
            TRANSFORM
        }

        public class CardForm
        {
            public CardForm(string textRepresentation)
            {
                TextRepresentation = textRepresentation;
            }

            public string TextRepresentation { get; private set; }
        }
    }
}
