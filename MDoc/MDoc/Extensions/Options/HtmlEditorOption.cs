namespace MDoc.Extensions.Options
{
    public class HtmlEditorOption
    {
        /// <summary>
        ///     list of plugins
        /// </summary>
        public string Plugin { get; set; }

        /// <summary>
        ///     Show the toolbar
        /// </summary>
        public bool? ShowToolbar { get; set; }

        /// <summary>
        ///     list of command on the toolbar
        /// </summary>
        public string Toolbar { get; set; }

        /// <summary>
        ///     show the menu bar ?
        /// </summary>
        public bool? ShowMenuBar { get; set; }

        /// <summary>
        ///     list of command on the menu bar
        /// </summary>
        public string MenuBar { get; set; }

        /// <summary>
        ///     theme
        /// </summary>
        public string Theme { get; set; }

        /// <summary>
        ///     custom css
        /// </summary>
        public string ContentCss { get; set; }

        /// <summary>
        ///     editor width
        /// </summary>
        public string Width { get; set; }

        /// <summary>
        ///     editor height
        /// </summary>
        public string Height { get; set; }

        public string PropertyName { get; set; }
        public string DefaultValue { get; set; }
    }
}