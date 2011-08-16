var Moon = Moon || {};

(function ($)
{

	Moon.MessageBubble = function (element)
	{

		var self = this;

		this.id = ''
		this.index = {};
		this.location = '';
		this.label = '';
		this.count = '';

		this.element = $(element);

		this.labelHeightOpen = 0;
		this.labelHeightClosed = 0;

		this.locationHeightOpen = 0;
		this.locationHeightClosed = 0;

		// An empty block for computing truncation
		this.empty = $("<div>").css("display", "block");

		// Check whether we can do css3 transitions
		this.csstransitions = $("html").hasClass("csstransitions");

		this.dom = {
			location: $(".location", this.element),
			label: $(".label", this.element),
			count: $($(".count", this.element).find("span")[0]),
			lower: $(".lower", this.element),
			text: $(".text", this.element),
			dot: $(".dot", this.element)
		};


		// Create some closures for callbacks
		this._onMouseClick = function (event)
		{
			self.onMouseClick(event);
		};
		this._onMouseOver = function (event)
		{
			self.onMouseOver(event);
		};
		this._onMouseOut = function (event)
		{
			self.onMouseOut(event);
		};

		this._appear = function ()
		{
			self.appear();
		};
		this._update = function ()
		{
			self.update();
		};

		// Store timeout ids
		this.delay = {
			update: null,
			show: null,
			hide: null
		};

		// Bind events

		this.element.bind("mouseenter", this._onMouseOver);
		this.element.bind("mouseleave", this._onMouseOut);
		this.element.bind("click", this._onMouseClick);

		if (this.csstransitions !== true)
		{
			// roll our own animations
			this.initContentSlide();
		}

		// Allow jQuery access to the Class state & methods
		this.element.data("MessageBubble", this);
		this.element.data("locked", false);
	}

	// Plays the message appear effect
	Moon.MessageBubble.prototype.appear = function (time)
	{

		var self = this;

		if (typeof (time) === "undefined")
		{
			time = 500;
		}

		this.element.css("visibility", "visible");

		this.dom.text.stop().delay(time).animate(
		{

			opacity: 1

		}, time * 0.5, function ()
		{

			self.dom.dot.attr("style", '');
		});

		if (this.csstransitions === true)
		{

			this.setTransition(this.dom.dot, "transform", time, "cubic-bezier(0.230, 1.000, 0.320, 1.000)");
			this.setScale(this.dom.dot, 1);

		}
		else
		{

			this.dom.dot.stop().animate(
			{
				opacity: 1
			}, time, "easeOutQuad");
		}

		setTimeout(function ()
		{
			self.element.addClass("enabled");
		}, time + 200);
	};

	// Plays the message disappear effect
	Moon.MessageBubble.prototype.disappear = function (time)
	{

		var self = this;

		if (typeof (time) === "undefined")
		{
			time = 500;
		}

		if (time === 0)
		{
			this.element.css("visibility", "hidden");
		}

		this.dom.text.stop().animate(
		{

			opacity: 0

		}, time * 0.5, function ()
		{

			if (self.csstransitions === true)
			{

				self.setTransition(self.dom.dot, "transform", time, "ease-in-out");
				self.setScale(self.dom.dot, 0);

			}
			else
			{

				self.dom.dot.stop().animate(
				{
					opacity: 0
				}, time, "easeInOutQuad");
			}

		});

		this.element.removeClass("enabled");
	};

	// Expands the message and reveals text / author
	Moon.MessageBubble.prototype.open = function ()
	{

		if (this.csstransitions !== true)
		{
			this.dom.label.stop().animate(
			{
				opacity: 1
			}, 150);
		}
	};

	// Contracts the message and hides text / author
	Moon.MessageBubble.prototype.close = function ()
	{

		if (this.csstransitions !== true)
		{
			this.dom.label.stop().animate(
			{
				opacity: 0
			}, 150);
		}
	};

	// Gets new content from service using ajax
	Moon.MessageBubble.prototype.refresh = function (data)
	{

		if (typeof (data) === "undefined")
		{
			return;
		}

		this.id = data.id;
		this.location = data.address;
		this.label = data.label;
		this.count = data.count;

		this.disappear();

		clearTimeout(this.delay.update);
		clearTimeout(this.delay.show);

		this.delay.update = setTimeout(this._update, 1200);
		this.delay.show = setTimeout(this._appear, 1200);
	};

	Moon.MessageBubble.prototype.initContentSlide = function ()
	{

		var self = this;

		this.labelHeightClosed = this.dom.label.height();
		this.locationHeightClosed = this.dom.location.height();

		// Open on mouseenter
		this.dom.lower.bind("mouseenter", function ()
		{

			if (self.element.hasClass("enabled") !== true)
			{
				return;
			}

			// If we don't yet know it, grab the hover height
			if (self.labelHeightOpen === 0)
			{
				self.labelHeightOpen = self.dom.label.height();
				self.dom.label.height(self.labelHeightClosed);
			}

			self.dom.label.stop().animate(
			{
				height: self.labelHeightOpen
			}, 250, "easeInOutQuad");
		});

		this.dom.location.bind("mouseenter", function ()
		{

			if (self.element.hasClass("enabled") !== true)
			{
				return;
			}

			if (self.locationHeightOpen === 0)
			{
				self.locationHeightOpen = self.dom.location.height();
				self.dom.location.height(self.locationHeightClosed);
			}

			self.dom.location.stop().animate(
			{
				height: self.locationHeightOpen
			}, 250, "easeInOutQuad");
		});

		// Close on mouseleave
		this.dom.lower.bind("mouseleave", function ()
		{

			self.dom.label.stop().animate(
			{
				height: self.labelHeightClosed
			}, 250, "easeInOutQuad");
		});

		this.dom.translated.bind("mouseleave", function ()
		{

			self.dom.location.stop().animate(
			{
				height: self.locationHeightClosed
			}, 250, "easeInOutQuad");
		});
	};

	Moon.MessageBubble.prototype.update = function ()
	{
		this.element.attr("href", "/devices/" + this.id);

		this.dom.label.text(this.label);
		this.dom.location.text(this.location);
		this.dom.count.text(this.count);

	};

	Moon.MessageBubble.prototype.truncate = function (text, container, numLines)
	{

		// Check the cache so we don't recompute
		if (typeof (this.index[text + numLines]) !== "undefined")
		{
			container.html(this.index[text + numLines]);
			return;
		}

		this.empty.css(
		{

			"line-height": container.css("line-height"),
			"font-family": container.css("font-family"),
			"text-align": container.css("text-align"),
			"font-size": container.css("font-size"),
			"width": container.css("width")

		});

		$("body").append(this.empty);
		this.empty.text(text);

		var lineHeight = parseInt(container.css("line-height"), 10);
		var max = lineHeight * numLines;
		var i = text.length;

		while (this.empty.height() > max && --i)
		{
			this.empty.text(text.substr(0, i));
		}

		var output;

		if (i !== text.length)
		{
			output = this.index[text + numLines] = text.substr(0, i - 3) + "&hellip;";
		}
		else
		{
			output = text;
		}

		this.index[text + numLines] = output;
		container.html(output);
		this.empty.remove();
	};

	Moon.MessageBubble.prototype.setTransition = function (element, prop, time, ease)
	{

		prop = prop || "all";
		time = time || "250";
		ease = ease || "ease-in-out";

		var v = (prop === "transform");

		var trans = prop + " " + time + "ms " + ease;

		element.css(
		{
			"-webkit-transition": v ? "-webkit-" + trans : trans,
			"-moz-transition": v ? "-moz-" + trans : trans,
			"-o-transition": v ? "-o-" + trans : trans,
			"transition": trans
		});
	};

	Moon.MessageBubble.prototype.setScale = function (element, scale)
	{

		var trans = "scale(" + scale + ")";

		element.css(
		{
			"-webkit-transform": trans,
			"-moz-transform": trans,
			"-o-transform": trans,
			"transform": trans
		});
	};

	// Handle events

	Moon.MessageBubble.prototype.onMouseClick = function (event)
	{
		//this.refresh();
	};

	Moon.MessageBubble.prototype.onMouseOver = function (event)
	{

		if (this.element.hasClass("enabled") !== true)
		{
			return;
		}

		this.element.data("locked", true);
		this.open();
	};

	Moon.MessageBubble.prototype.onMouseOut = function (event)
	{

		if (this.element.hasClass("enabled") !== true)
		{
			return;
		}

		this.element.data("locked", false);
		this.close();
	};

	// jQuery plugin declaration

	$.fn.messageBubble = function ()
	{
		return this.each(function ()
		{
			(new Moon.MessageBubble(this));
		});
	}

})(jQuery);