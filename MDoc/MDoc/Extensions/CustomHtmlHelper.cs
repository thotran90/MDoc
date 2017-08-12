using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using MDoc.Extensions.Options;

namespace MDoc.Extensions
{
    public static class CustomHtmlHelper
    {
        #region Modal

        public static MvcHtmlString Modal(this HtmlHelper htmlHelper, string action, string buttonText, string icon,
            ModalOption modalOption, IDictionary<string, object> htmlAttributes = null)
        {
            var button = new TagBuilder("a");
            button.Attributes.Add("data-toggle", "modal");
            button.Attributes.Add("data-target", modalOption.TargetId);
            button.AddCssClass(modalOption.ButtonStyle);
            button.Attributes.Add("href", action);
            button.Attributes.Add("data-show", modalOption.Show.ToString().ToLower());
            button.Attributes.Add("data-backdrop", modalOption.BackDrop.ToString().ToLower());
            var tagIcon = new TagBuilder("i");
            if (!string.IsNullOrEmpty(icon))
            {
                tagIcon.Attributes.Add("class", "fa fa-" + icon);
                button.InnerHtml = tagIcon + " " + buttonText;
            }
            else
            {
                button.InnerHtml = buttonText;
            }
            // Apply Html Attribute
            var attributes = htmlHelper.AttributeEncode(htmlAttributes);
            if (!string.IsNullOrEmpty(attributes))
            {
                attributes = attributes.Trim('{', '}');
                var attrValuePairs = attributes.Split(',');
                foreach (var attrValuePair in attrValuePairs)
                {
                    var equalIndex = attrValuePair.IndexOf('=');
                    var attr = attrValuePair.Substring(0, equalIndex);
                    button.Attributes.Add(attrValuePair.Substring(0, equalIndex),
                        attrValuePair.Substring(equalIndex + 1));
                }
            }
            var html = button.ToString();
            return MvcHtmlString.Create(html);
        }

        #endregion

        #region Select2

        /// <summary>
        ///     Generate a dropdown list for displaying asynchronously data fetched from the server, at the set url.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name">Input name</param>
        /// <param name="url">URL source to query on</param>
        /// <param name="select2Option">Behavior options for the dropdown list</param>
        /// <param name="htmlAttributes">Html attributes for the rendered dropdown</param>
        /// <returns></returns>
        public static MvcHtmlString Select2Ajax(this HtmlHelper helper, string name, string url,
            Select2Option select2Option = null, object htmlAttributes = null)
        {
            select2Option = (select2Option ?? new Select2Option());
            select2Option.PropertyName = name;
            select2Option.Ajax = true;
            return GetSelect2(helper, select2Option, null, url, htmlAttributes);
        }

        /// <summary>
        ///     Generic method to generate a dropdown list for displaying asynchronously data fetched from the server, at the set
        ///     url.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression">
        ///     Accessor for the attribute to alter. Define the input name if not overriden in the html
        ///     attributes. Define the default value if the property is not null.
        /// </param>
        /// <param name="url">URL source to query on</param>
        /// <param name="select2Option">Behavior options for the dropdown list</param>
        /// <param name="htmlAttributes">Html attributes for the rendered dropdown</param>
        /// <returns></returns>
        public static MvcHtmlString Select2AjaxFor<TModel, TValue>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> expression, string url, Select2Option select2Option = null,
            object htmlAttributes = null)
        {
            select2Option = (select2Option ?? new Select2Option());
            if (helper.ViewData.Model != null)
            {
                var text = string.Concat(expression.Compile()(helper.ViewData.Model));
                if (text != "" && text != "0")
                {
                    select2Option.DefaultValue = text;
                }
            }
            select2Option.PropertyName = ExpressionHelper.GetExpressionText(expression);
            select2Option.Ajax = true;
            return GetSelect2(helper, select2Option, null, url, htmlAttributes);
        }

        /// <summary>
        ///     Generic method to display a dropdown list on page load with the select list passed as parameters.
        /// </summary>
        /// <typeparam name="TModel">object</typeparam>
        /// <typeparam name="TValue">object</typeparam>
        /// <param name="helper"></param>
        /// <param name="expression">
        ///     Accessor for the attribute to alter. Define the input name if not overriden in the html
        ///     attributes. Define the default value if the property is not null.
        /// </param>
        /// <param name="selectList">
        ///     Select list of values. The selected options will define the default value if the attribute
        ///     value is null.
        /// </param>
        /// <param name="select2Option">Behavior options for the dropdown list</param>
        /// <param name="htmlAttributes">Html attributes for the rendered dropdown</param>
        /// <returns></returns>
        public static MvcHtmlString Select2For<TModel, TValue>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList,
            Select2Option select2Option = null, object htmlAttributes = null)
        {
            select2Option = (select2Option ?? new Select2Option());
            if (helper.ViewData.Model != null)
            {
                var text = string.Concat(expression.Compile()(helper.ViewData.Model));
                if (text != "" && text != "0")
                {
                    select2Option.DefaultValue = text;
                }
            }
            select2Option.PropertyName = ExpressionHelper.GetExpressionText(expression);
            return GetSelect2(helper, select2Option, selectList, "", htmlAttributes);
        }

        /// <summary>
        ///     Generate a dropdown list on page load with the select list passed as parameters.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name">Input name</param>
        /// <param name="selectList">Select list of values. The selected options will define the default value.</param>
        /// <param name="select2Option">Behavior options for the dropdown list</param>
        /// <param name="htmlAttributes">Html attributes for the rendered dropdown</param>
        /// <returns></returns>
        public static MvcHtmlString Select2(this HtmlHelper helper, string name, IEnumerable<SelectListItem> selectList,
            Select2Option select2Option = null, object htmlAttributes = null)
        {
            select2Option = (select2Option ?? new Select2Option());
            select2Option.PropertyName = name;
            return GetSelect2(helper, select2Option, selectList, "", htmlAttributes);
        }

        /// <summary>
        ///     Build a select2 with the options. If no URL are provided in an AJAX select2, throw an exception.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="select2Option">Behavior options for the dropdown list</param>
        /// <param name="selectList">Select list of values. The selected options will define the default value.</param>
        /// <param name="url">URL source to query on</param>
        /// <param name="htmlAttributes">Html attributes for the rendered dropdown</param>
        /// <returns></returns>
        private static MvcHtmlString GetSelect2(HtmlHelper helper, Select2Option select2Option,
            IEnumerable<SelectListItem> selectList, string url, object htmlAttributes = null)
        {
            var htmlFieldPrefix = helper.ViewData.TemplateInfo.HtmlFieldPrefix;
            //.get_ViewData().get_TemplateInfo().get_HtmlFieldPrefix();
            if (!string.IsNullOrEmpty(htmlFieldPrefix))
            {
                select2Option.PropertyName = string.Format("{0}.{1}", htmlFieldPrefix, select2Option.PropertyName);
            }
            var tagBuilder = select2Option.Ajax ? new TagBuilder("input") : new TagBuilder("select");
            tagBuilder.Attributes.Add("name", select2Option.PropertyName);
            tagBuilder.Attributes.Add("id", select2Option.PropertyName.Replace(".", "_"));
            var modelMetadata = ModelMetadata.FromStringExpression(select2Option.PropertyName, helper.ViewData);
            var unobtrusiveValidationAttributes = helper.GetUnobtrusiveValidationAttributes(select2Option.PropertyName,
                modelMetadata);
            foreach (var current in unobtrusiveValidationAttributes.Keys)
            {
                tagBuilder.Attributes.Add(current, unobtrusiveValidationAttributes[current].ToString());
            }
            if (select2Option.MinimumInputLength.HasValue)
            {
                tagBuilder.Attributes.Add("data-min-length", select2Option.MinimumInputLength.ToString());
            }
            if (select2Option.MaximumInputLength.HasValue)
            {
                tagBuilder.Attributes.Add("data-max-length", select2Option.MaximumInputLength.ToString());
            }
            if (select2Option.Placeholder != null)
            {
                tagBuilder.Attributes.Add("data-placeholder", select2Option.Placeholder);
            }
            else
            {
                select2Option.Placeholder = string.Format("Select a(n) {0}",
                    (!string.IsNullOrEmpty(modelMetadata.DisplayName)) ? modelMetadata.DisplayName : "item");
                tagBuilder.Attributes.Add("data-placeholder", select2Option.Placeholder);
            }
            if (select2Option.DefaultValue != null)
            {
                tagBuilder.Attributes.Add("value", select2Option.DefaultValue);
            }
            if (select2Option.Width != null)
            {
                tagBuilder.Attributes.Add("data-width", select2Option.Width);
            }
            if (select2Option.AllowClear.HasValue)
            {
                tagBuilder.Attributes.Add("data-allowclear", select2Option.AllowClear.ToString().ToLower());
            }
            if (select2Option.Multiple)
            {
                tagBuilder.Attributes.Add("multiple", "multiple");
                if (select2Option.MaximumSelectionSize.HasValue)
                {
                    tagBuilder.Attributes.Add("data-maximum-selection-size",
                        select2Option.MaximumSelectionSize.Value.ToString());
                }
            }
            if (select2Option.Disabled)
            {
                tagBuilder.Attributes.Add("disabled", "disabled");
            }
            if (!string.IsNullOrEmpty(select2Option.OnChange))
            {
                tagBuilder.Attributes.Add("data-onChange", select2Option.OnChange);
            }
            if (!string.IsNullOrEmpty(select2Option.DependsOn))
            {
                tagBuilder.Attributes.Add("data-dependsOn", select2Option.DependsOn);
            }
            if (select2Option.QuietMillis > 0)
            {
                tagBuilder.Attributes.Add("data-quietmillis", select2Option.QuietMillis.ToString());
            }
            if (select2Option.Ajax)
            {
                if (string.IsNullOrEmpty(url))
                {
                    throw new ArgumentNullException("url", "If AJAX option is set to true, an URL must be provided.");
                }
                tagBuilder.Attributes.Add("data-action", url);
            }
            if (htmlAttributes != null)
            {
                var routeValueDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                tagBuilder.MergeAttributes(routeValueDictionary, true);
            }
            tagBuilder.AddCssClass("toSelect2");
            if (select2Option.Ajax)
            {
                tagBuilder.AddCssClass("select2-ajax");
            }
            if (selectList != null && !select2Option.Ajax)
            {
                var list = selectList.ToList();
                if (string.IsNullOrEmpty(select2Option.DefaultValue))
                {
                    var tagBuilder2 = new TagBuilder("option");
                    var expr_3AF = tagBuilder;
                    expr_3AF.InnerHtml = expr_3AF.InnerHtml + tagBuilder2;
                }
                foreach (var current2 in list)
                {
                    var tagBuilder3 = new TagBuilder("option");
                    if (current2.Selected || current2.Value == select2Option.DefaultValue)
                    {
                        tagBuilder3.Attributes.Add("selected", "selected");
                    }
                    tagBuilder3.Attributes.Add("value", current2.Value);
                    tagBuilder3.InnerHtml = current2.Text;
                    var expr_443 = tagBuilder;
                    expr_443.InnerHtml = expr_443.InnerHtml + tagBuilder3;
                }
            }
            return new MvcHtmlString(tagBuilder.ToString());
        }

        #endregion

        #region WYSIWYG Editor - Tinymce

        internal static MvcHtmlString WysiwygEditor(this HtmlHelper helper, HtmlEditorOption editorOption,
            object htmlAttributes = null)
        {
            var tagBuilder = new TagBuilder("textarea");
            if (htmlAttributes != null)
            {
                var routeValueDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                tagBuilder.MergeAttributes(routeValueDictionary, true);
            }

            tagBuilder.Attributes.Add("name", editorOption.PropertyName);
            tagBuilder.Attributes.Add("id", editorOption.PropertyName);
            var modelMetadata = ModelMetadata.FromStringExpression(editorOption.PropertyName, helper.ViewData);
            var unobtrusiveValidationAttributes = helper.GetUnobtrusiveValidationAttributes(editorOption.PropertyName,
                modelMetadata);
            foreach (var current in unobtrusiveValidationAttributes.Keys)
            {
                tagBuilder.Attributes.Add(current, unobtrusiveValidationAttributes[current].ToString());
            }
            if (htmlAttributes != null)
            {
                var routeValueDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                tagBuilder.MergeAttributes(routeValueDictionary, true);
            }

            if (editorOption.ShowMenuBar.HasValue)
            {
                tagBuilder.Attributes.Add("data-showmenubar", editorOption.ShowMenuBar.Value.ToString().ToLower());
            }
            if (!string.IsNullOrEmpty(editorOption.MenuBar))
            {
                tagBuilder.Attributes.Add("data-menubar", editorOption.MenuBar);
            }
            if (editorOption.ShowToolbar.HasValue)
            {
                tagBuilder.Attributes.Add("data-showtoolbar", editorOption.ShowToolbar.Value.ToString().ToLower());
            }
            if (!string.IsNullOrEmpty(editorOption.Toolbar))
            {
                tagBuilder.Attributes.Add("data-toolbar", editorOption.Toolbar);
            }
            if (!string.IsNullOrEmpty(editorOption.Plugin))
            {
                tagBuilder.Attributes.Add("data-plugin", editorOption.Plugin);
            }
            if (!string.IsNullOrEmpty(editorOption.Theme))
            {
                tagBuilder.Attributes.Add("data-theme", editorOption.Theme);
            }
            if (!string.IsNullOrEmpty(editorOption.ContentCss))
            {
                tagBuilder.Attributes.Add("data-contentcss", editorOption.ContentCss);
            }
            if (!string.IsNullOrEmpty(editorOption.Width))
            {
                tagBuilder.Attributes.Add("data-width", editorOption.Width.Trim());
            }
            if (!string.IsNullOrEmpty(editorOption.Height))
            {
                tagBuilder.Attributes.Add("data-height", editorOption.Height.Trim());
            }
            if (!string.IsNullOrEmpty(editorOption.DefaultValue))
            {
                tagBuilder.InnerHtml = editorOption.DefaultValue;
            }


            tagBuilder.AddCssClass("tinymce");
            return new MvcHtmlString(tagBuilder.ToString());
        }

        public static MvcHtmlString HtmlEditor(this HtmlHelper helper, string name, string defaultValue = null,
            HtmlEditorOption option = null,
            object htmlAttribute = null)
        {
            if (option == null) option = new HtmlEditorOption();
            option.PropertyName = name;
            option.DefaultValue = defaultValue;
            return WysiwygEditor(helper, option, htmlAttribute);
        }

        public static MvcHtmlString HtmlEditorFor<TModule, TValue>(this HtmlHelper<TModule> helper,
            Expression<Func<TModule, TValue>> expression, HtmlEditorOption options = null, object htmlAttribute = null)
        {
            if (options == null) options = new HtmlEditorOption();
            if (helper.ViewData.Model != null)
            {
                var text = string.Concat(expression.Compile()(helper.ViewData.Model));
                if (!string.IsNullOrEmpty(text))
                {
                    options.DefaultValue = text;
                }
            }
            options.PropertyName = ExpressionHelper.GetExpressionText(expression);
            return WysiwygEditor(helper, options, htmlAttribute);
        }

        #endregion

    }
}