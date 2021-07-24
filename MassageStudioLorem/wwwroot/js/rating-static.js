var $star_rating = $(".star-rating .fa");

var SetRatingStar = function () {
    return $star_rating.each(function () {
        if (parseInt($star_rating.siblings("input.rating-value").val()) >= parseInt($(this).data("rating"))) {
            return $(this).removeClass("far fa-star-o")
                .addClass("far fa-star");
        } else {
            return $(this).removeClass("far fa-star")
                .addClass("far fa-star-o");
        }
    });
};

SetRatingStar();