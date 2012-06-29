/* this library handles backwards-compatability of the HTML5 placeholder attribute
all this is from https://gist.github.com/588988 and http://borderstylo.com/posts/213-html5-feature-detection-and-adding-support-for-the-placeholder-attribute */

// feature-detection.js:
var textareaSupport = function(){
  var support = {},
      t = document.createElement('textarea');

  support.placeholder = (t.placeholder !== undefined);

  return support;
};

Modernizr.textarea = textareaSupport();

// jquery.placeholder.js:

$.fn.supportPlaceHolder = function(){
  return this.each(function(){
    var element = this;

    $(element).each(function(i){
      $(this).val($(this).attr('placeholder'));

      $(this).focus(function(e){
        if ($(this).val() === $(this).attr('placeholder')) {
          $(this).val('');
        }
      });

      $(this).blur(function(e){
        if ($(this).val() === '') {
          $(this).val($(this).attr('placeholder'));
        }
      });
    });

    $('form').submit(function(e){
      $(this).find(element).each(function(i){
        if ($(this).val() === $(this).attr('placeholder')) {
          $(this).val('');
        }
      });
    });
  });
}


// application.js:

$(window).load(function(){
  if (!Modernizr.input.placeholder){
    $('input').supportPlaceHolder();
  }

  if (!Modernizr.textarea.placeholder){
    $('textarea').supportPlaceHolder();
  }
});