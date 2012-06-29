using System;
using Lucene.Net.Documents;
using System.IO;
using System.Linq;
using Legato.Helpers;
namespace Legato.Models.Search
{
public static class Types
{
public static Document DynamicToDocument(dynamic r)
{
	var doc = new Document();
            if (r is Instrument)
            {
                var p = (Instrument)r;
                p.FillProperties();

                doc.Add(new Field("Title", new StringReader(p.Title)));
                doc.Add(new Field("Text", new StringReader(p.Markdown)));
                doc.Add(new Field("RawTitle", p.Title, Field.Store.YES, Field.Index.UN_TOKENIZED));
                doc.Add(new Field("RawText", p.Markdown, Field.Store.YES, Field.Index.UN_TOKENIZED));
                doc.Add(new Field("Type", new StringReader(MagicCategoryStrings.Instrument)));
                doc.Add(new Field("PostID", Convert.ToString(p.InstrumentID), Field.Store.YES, Field.Index.UN_TOKENIZED));
                doc.Add(new Field("GlobalPostID", Convert.ToString(p.GlobalPostID), Field.Store.YES, Field.Index.UN_TOKENIZED));
                doc.Add(new Field("AuthorID", Convert.ToString(p.UserID), Field.Store.YES, Field.Index.UN_TOKENIZED));

                //var sb = new StringBuilder();
                //foreach (var tag in p.PostTags)
                //{
                //    sb.Append("<" + tag.Tag.TagName + "> ");
                //}
                //doc.Add(new Field("RawTags", sb.ToString().Trim(), Field.Store.YES, Field.Index.UN_TOKENIZED));
            }
            else if (r is InstrumentReview)
            {
                var p = (InstrumentReview)r;
                p.FillProperties();
                var ins = p.Instrument;
                ins.FillProperties();

                doc.Add(new Field("Title", new StringReader(p.Title)));
                doc.Add(new Field("Text", new StringReader(p.Revisions.First().MessageHTML.ConvertHtmlIntoText())));
                doc.Add(new Field("RawTitle", p.Title, Field.Store.YES, Field.Index.UN_TOKENIZED));
                doc.Add(new Field("RawText", p.Revisions.First().MessageHTML.ConvertHtmlIntoText(), Field.Store.YES, Field.Index.UN_TOKENIZED));
                doc.Add(new Field("Type", new StringReader(MagicCategoryStrings.InstrumentReview)));
                doc.Add(new Field("PostID", Convert.ToString(p.ReviewID), Field.Store.YES, Field.Index.UN_TOKENIZED));
                doc.Add(new Field("GlobalPostID", Convert.ToString(p.GlobalPostID), Field.Store.YES, Field.Index.UN_TOKENIZED));
                doc.Add(new Field("AuthorID", Convert.ToString(p.UserID), Field.Store.YES, Field.Index.UN_TOKENIZED));

                //var sb = new StringBuilder();
                //foreach (var tag in p.PostTags)
                //{
                //    sb.Append("<" + tag.Tag.TagName + "> ");
                //}
                //doc.Add(new Field("RawTags", sb.ToString().Trim(), Field.Store.YES, Field.Index.UN_TOKENIZED));
            }
	return doc;
}
public static int DynamicToDocumentId(dynamic r)
{
	if (r is Instrument)
            {
                return r.InstrumentID;
            }
            else if (r is InstrumentReview)
            {
                return r.ReviewID;
            }
			
	return -1;
}


}


}


