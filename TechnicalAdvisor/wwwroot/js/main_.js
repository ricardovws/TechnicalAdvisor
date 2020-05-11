$(".tooltip").tooltipster({
    theme: "tooltipster-noir"
});

$("input").focus();

$("#search-button").click(showSearchList);
$("#search-button_again").click(searchAgain);




function showSearchList() {

    
    $(".listOfResults").stop().slideToggle();
    $(".search_box").toggle();
    //$(".search_box").css("display", "none");
    var scrollList = $(".navbar-fixed-top").offset().bottom;
    $("html, body").animate(
        {
            scrollTop: scrollList + "px"
        }, 500
    )
    
    $(".main_search_box").css("height", "20vh");
    $("body").css("overflow", "auto");
    }

function searchAgain() {
    $(".main_search_box").css("height", "100vh");
    $(".listOfResults").toggle(500);
    $(".search_box").toggle(500);
    //$(".search_box").css("display", "initial");
    var scrollList = $(".search_box").offset().bottom;
    $("html, body").animate(
        {
            scrollTop: scrollList + "px"
        }, 500
    )
    $("input").val('');
    $("input").focus();
    $("body").css("overflow", "hidden");
}    
