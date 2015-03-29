(function ($) {

    //********************************************************************************
    //  Methods to be used internally
    //********************************************************************************
    var methods = {
        //********************************************************************************
        //  Method to calculate current percent width value of object to parent container
        //********************************************************************************
        get_percent_width: function (element_width, container_width) {
            //calculate percentage value
            return parseFloat(element_width) / parseFloat(container_width);
        },
        //********************************************************************************
        //  Method to check whether or not items need to be resized.
        //********************************************************************************
        check_resize_needed: function (parent_height, container_height) {
            if (
                //check whether or not the panel height is greater than the parent container still
                parent_height < container_height

            ) {
                return true;
            } else {
                return false;
            }
        }
    };

    //********************************************************************************
    //  jQuery Responsive Image Menu
    //********************************************************************************
    $.fn.responsiveImageMenu = function (options) {
        return this.each(function () {
            var settings = $.extend({
                //put defaults here
                'aspect_ratio': 1,
                'list_tag': 'span'

            }, options);

            //get elements into a local var
            var container = $(this);
            var list_items = container.find(settings.list_tag);
            var first_item = list_items.first();

            //reset width to 100% when reinitializing
            list_items.css("width", "100%");

            //set height to current width using aspect ratio - this is just in case some browsers take time to load an image
            list_items.css("height", first_item.width() * settings.aspect_ratio);

            //loop while resize is needed.
            var parent_height = container.parent().height();
            var container_height = container.height();
            var container_width = container.width();
            var current_item_width = first_item.width();

            while (methods.check_resize_needed(parent_height, container_height)) {
                current_item_width = Math.floor((methods.get_percent_width(current_item_width, container_width) - .01) * container_width);
                container_height = list_items.length * current_item_width;
            }
            list_items.css("width", current_item_width);
            list_items.css("height", current_item_width * settings.aspect_ratio);
        });
    };
}(jQuery));

$(function () {
    $("#panel").responsiveImageMenu({
        aspect_ratio: 1,
        list_tag: 'span'
    });
    $("#panel2").responsiveImageMenu({
        aspect_ratio: 1,
        list_tag: 'span'
    });
});

