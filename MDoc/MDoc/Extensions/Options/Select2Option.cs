namespace MDoc.Extensions.Options
{
    public class Select2Option
    {
        /// <summary>
        ///     Initialize QuietMillis by default to 300ms.
        /// </summary>
        public Select2Option()
        {
            QuietMillis = 300;
        }

        /// <summary>
        ///     Name of the input property. This name will be used when submitting this input in the form.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        ///     Default value set. If AJAX option is to true, will fetch asynchronously the result on page load.
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        ///     Prepend a new option in the list with a certain value passed as parameter.
        /// </summary>
        public string NullValue { get; set; }

        /// <summary>
        ///     Specify an input id to depends on (ex: an other select2). On change of the first input, its value will be injected
        ///     in the query string of the second.
        /// </summary>
        public string DependsOn { get; set; }

        /// <summary>
        ///     Number of characters necessary to start a search.
        /// </summary>
        public int? MinimumInputLength { get; set; }

        /// <summary>
        ///     Maximum number of characters that can be entered for an input.
        /// </summary>
        public int? MaximumInputLength { get; set; }

        /// <summary>
        ///     Controls the width style attribute of the Select2 container div.
        ///     For predefined values : Use the enum WithType
        ///     For other values : If the width attribute contains a function it will be evaluated, otherwise the value is used
        ///     verbatim.
        /// </summary>
        public string Width { get; set; }

        /// <summary>
        ///     Initial value that is selected if no other selection is made.
        ///     Note that because browsers assume the first option element is selected in non-multi-value select boxes an empty
        ///     first option element must be provided (<option></option>) for the placeholder to work.
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        ///     Whether or not a clear button is displayed when the select box has a selection. The button, when clicked, resets
        ///     the value of the select box back to the placeholder, thus this option is only available when the placeholder is
        ///     specified.
        ///     This option only works when the placeholder is specified.
        ///     When attached to a select an option with an empty value must be provided. This is the option that will be selected
        ///     when the button is pressed since a select box requires at least one selection option.
        ///     Also, note that this option only works with non-multi-value based selects because multi-value selects always
        ///     provide such a button for every selected option.
        /// </summary>
        public bool? AllowClear { get; set; }

        /// <summary>
        ///     Whether or not Select2 allows selection of multiple values.
        ///     When Select2 is attached to a select element this value will be ignored and select's multiple attribute will be
        ///     used instead.
        /// </summary>
        public bool Multiple { get; set; }

        /// <summary>
        ///     Only applies when configured in multi-select mode.
        ///     If set to false the dropdown is not closed after a selection is made, allowing for rapid selection of multiple
        ///     items. By default this option is set to true.
        /// </summary>
        public bool CloseOnSelect { get; set; }

        /// <summary>
        ///     Only applies when configured in multi-select mode.
        ///     Specify the maximum number of selectable items allowed in a multiple select list.
        /// </summary>
        public int? MaximumSelectionSize { get; set; }

        /// <summary>
        ///     todo : write doc here
        /// </summary>
        public bool Ajax { get; set; }

        public bool Disabled { get; set; }

        /// <summary>
        ///     The javascript method called on OnChange
        /// </summary>
        public string OnChange { get; set; }

        /// <summary>
        ///     Number of milliseconds to wait for the user to stop typing before issuing the ajax request.
        /// </summary>
        public int QuietMillis { get; set; }
    }
}