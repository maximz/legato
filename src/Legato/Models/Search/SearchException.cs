using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Legato.Models.Search
{
	/// <summary>
	/// The exception that is thrown when a search related task does not complete correctly.
	/// </summary>
	public class SearchException : ExceptionBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SearchException"/> class.
		/// </summary>
		/// <param name="message">The exception message.</param>
		/// <param name="inner">The inner exception.</param>
		public SearchException(string message, Exception inner) : base(message, inner) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="SearchException"/> class.
		/// </summary>
		/// <param name="inner">The inner exception.</param>
		/// <param name="message">The message as an <see cref="IFormattable"/> string.</param>
		/// <param name="args">Arguments for the message format.</param>
		public SearchException(Exception inner, string message, params object[] args) : base(string.Format(message, args), inner) { }
	}

	/// <summary>
	/// The exception that is thrown when a spatial search related task does not complete correctly.
	/// </summary>
	public class SpatialSearchException : ExceptionBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SpatialSearchException"/> class.
		/// </summary>
		/// <param name="message">The exception message.</param>
		/// <param name="inner">The inner exception.</param>
		public SpatialSearchException(string message, Exception inner) : base(message, inner) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="SpatialSearchException"/> class.
		/// </summary>
		/// <param name="inner">The inner exception.</param>
		/// <param name="message">The message as an <see cref="IFormattable"/> string.</param>
		/// <param name="args">Arguments for the message format.</param>
		public SpatialSearchException(Exception inner, string message, params object[] args) : base(string.Format(message, args), inner) { }
	}
}
