/*GLOBAL VARIABLES*/
var isInputNameValid, isInputCityValid;
var isContactInfoUp;

$(document).ready(function () {
    $("#lstCities").empty();    // weirdest fix ever -> input field no move

    $("#SearchCity").on("input propertychange paste", function () {
        var currentInput = $(this).val();
        loadCities(currentInput);
        calculateLatLngCity(currentInput);
    });

    $("#add-store-city").on("input propertychange paste", function () {
        var currentInput = $(this).val();
        loadCities(currentInput);
    });;

    $(document).scroll(function () {
        var offset = $("nav").offset().top;

        // give navigation border when scrolling down
        if (offset > 0) {
            $("nav").addClass("nav-active");
        }
        else {
            $("nav").removeClass("nav-active");
        }
    });

    $("#search-form").submit(function (e) {
        // check input
        if ($("#SearchName").val() == "") {
            isInputNameValid = false;
        }
        else {
            isInputNameValid = true;
        }

        if ($("#SearchCity").val() == "") {
            isInputCityValid = false;
        }
        else {
            isInputCityValid = true;
        }

        // submit if form is valid
        if (isInputNameValid || isInputCityValid) {
            return true;
        }
        else {
            // do shake implementeren

            e.preventDefault();
            return false;
        }
    });

    $(".detail-contact ul li svg").click(function () {
        var target = $(this).attr("class");
        var selector = "." + target + " path";
        var className = target + "-active";

        if (!isContactInfoUp) {
            isContactInfoUp = true;
            animateDetailContactUp(selector, className);

            var content = "." + target.replace("icon", "popup");
            $(content).css("display", "block");
        }
        else {
            isContactInfoUp = false;
            animateDetailContactDown();
        }
    });

    $(".detail-contact ul li svg").hover(function () {
        var target = $(this).attr("class");
        var selector = "." + target + " path";
        var className = target + "-active";

        // get lunar target
        var svgTarget = document.querySelector(selector);
        var isSVGActive = lunar.hasClass(svgTarget, className);

        if (isContactInfoUp && !isSVGActive) {
            isContactInfoUp = false;
            animateDetailContactDown();
        }
    }, function () {

    });

    $(".detail-pictures ul li").hover(function () {
        $(this).find("div").css("top", "-124px");
    }, function () {
        $(this).find("div").css("top", "0px");
    });

    $("#search-store").change(function () {
        // get values
        var filter = $(this).val();
        var sort = $("#sort-store").find(":selected").val();

        loadCompanies(filter, sort);
    });

    $("#sort-store").change(function () {
        // get values
        var filter = $("#search-store").val();
        var sort = $(this).find(":selected").val();

        loadCompanies(filter, sort);
    });

    $("#add-store-address").change(function () {
        // get value
        var address = $(this).val();
        var city = " " + $("#add-store-city").val();

        if (city != "" && address != "") {
            address += city;
        }

        calculateLatLng(address);
    });

    $("#add-store-avatar").change(function () {
        var imageFile = this.files[0];

        var url = window.URL.createObjectURL(imageFile);

        var img = document.getElementById("add-store-avatar-preview");
        img.src = url;
    });

    $("#add-store-florist-avatar").change(function () {
        var imageFile = this.files[0];

        var url = window.URL.createObjectURL(imageFile);

        var img = document.getElementById("add-store-avatar-florist-preview");
        img.src = url;
    });

    $("#add-store-images").change(function () {
        var htmlList = "";
        for (var i = 0; i < this.files.length; i++) {
            htmlList += "<li>" + this.files[i].name + "</li>";
        }

        $("#store-images-list").empty().css("display", "block").append(htmlList);
    });

    $("#side-blog-search-form-submit").click(function () {
        $("#side-blog-search-form").submit();
    });
});

/*FUNCTIONS*/
function animateDetailContactUp(selector, className) {
    $(".detail-contact ul li svg path").each(function () {                                  // remove previous classes
        $(this).removeAttr("class");
    });
    $("div#detail-contact-popup *").css("display", "none");

    $(selector).attr("class", className);                                                   // change color
    $("#detail-contact-popup").removeAttr("class");
    $("#detail-contact-popup").addClass(className);

    $(".detail-contact ul").stop().animate({ top: "-40px" }, 600, "easeInOutQuint");        // move up
    $("#detail-contact-popup").stop().animate({ top: "-40px" }, 600, "easeInOutQuint");
}

function animateDetailContactDown() {
    $(".detail-contact ul li svg path").each(function () {                                  // remove previous classes
        $(this).removeAttr("class");
    });

    $(".detail-contact ul").stop().animate({ top: "0px" }, 600, "easeInOutQuint");          // move down
    $("#detail-contact-popup").stop().animate({ top: "0px" }, 600, "easeInOutQuint");
}

//function loadCities(currentInput) {
//    // clear list if no characters in input field
//    if (currentInput.length < 3) {
//        $("#lstCities").empty();
//    }

//    // get data if 3 characters in input field
//    if (currentInput.length == 3) {
//        // get api data
//        $.getJSON("/api/cities?city=" + currentInput, function (data) {
//            var polyfillData = "";

//            for (var i = 0; i < data.length; i++) {
//                var city = {
//                    value: data[i].City,
//                    id: data[i].ID
//                };

//                // append data to datalist
//                var datalistItem = "<option id='" + city.id + "' value='" + city.value + "'></option>";
//                $("#lstCities").append(datalistItem);

//                polyfillData += datalistItem;
//            }

//            $("#lstCities").htmlPolyfill(polyfillData);;
//        });
//    }
//}

function loadCompanies(filter, sort) {
    $("#store-info img").css("display", "block");
    $("#store-list").empty();

    $.getJSON("/api/companies?filter=" + filter + "&sort=" + sort, function (data) {
        for (var i = 0; i < data.length; i++) {
            var company = {
                ID: data[i].ID,
                Name: data[i].Name,
                City: data[i].Location.City,
                Province: data[i].Location.Province.Name
            }

            // add to page
            var profile = '<div class="store-profile"><p>' + company.Name + '</p><p>' + company.City + '</p><p>' + company.Province + '</p><ul><li><a href="/Detail?profile=' + company.ID + '"><svg class="icon-info" version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" viewBox="0 0 310 310" enable-background="new 0 0 310 310" xml:space="preserve"><rect fill="#cccccc" width="310" height="310"/><polygon fill="#ffffff" points="107.9,263.5 104.2,260.4 199,155 104.2,49.6 107.9,46.5 205.8,155 "/></svg></a></li><li><a href="/Manage/EditStore?id=' + company.ID + '" title="Edit"><svg class="icon-edit" version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" viewBox="0 0 50 50" enable-background="new 0 0 50 50" xml:space="preserve"><rect fill="#cccccc" width="50" height="50"/><g><path fill="#ffffff" d="M12.5,37.5l2.7-0.4l-2.4-2.4L12.5,37.5z M12.5,37.5"/><g><path fill="#ffffff" d="M13.3,31.6L13,33.7l3.3,3.3l2.1-0.3L33,22.2L27.8,17L13.3,31.6z M13.3,31.6"/><path fill="#ffffff" d="M33.4,12.8c-0.2-0.2-0.4-0.3-0.6-0.3c-0.2,0-0.5,0.1-0.7,0.3l-3.5,3.5l5.2,5.1l3.5-3.5 c0.4-0.4,0.4-1,0-1.3L33.4,12.8z M33.4,12.8"/></g></g></svg></a></li><li><a href="/Manage/DeleteStore?id=' + company.ID + '" title="Delete"><svg class="icon-delete" version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" viewBox="0 0 50 50" enable-background="new 0 0 50 50" xml:space="preserve"><rect fill="#cccccc" width="50" height="50"/><g><path fill="#ffffff" d="M32.7,22.7L32.7,22.7C32.7,22.8,32.7,22.8,32.7,22.7c0,0.1,0,0.1,0,0.1l-0.6,12.2h0c-0.1,1-1.2,2.4-7.1,2.4 c-5.9,0-7-1.5-7.1-2.4h0l-0.6-12.2c0,0,0,0,0,0c0,0,0,0,0,0l0-0.1h0c0-0.1,0.1-0.2,0.2-0.3c0.8,0.8,3.8,0.9,7.5,0.9 c3.7,0,6.7-0.1,7.5-0.9C32.6,22.5,32.7,22.6,32.7,22.7L32.7,22.7z M33.7,18.1v1.4c0,0.3-0.3,0.6-0.8,0.8c-1.4,0.7-4.4,1.1-7.9,1.1 c-3.5,0-6.5-0.5-7.9-1.1c-0.5-0.3-0.8-0.5-0.8-0.8v-1.4c0-0.8,1.9-1.4,4.8-1.7v-3c0-0.5,0.4-0.9,0.9-0.9h5.9c0.5,0,0.9,0.4,0.9,0.9 v3C31.6,16.6,33.7,17.3,33.7,18.1L33.7,18.1z M27.1,15c0-0.5-0.1-1-0.2-1h-4.1c-0.1,0-0.2,0.4-0.2,1v1.2l0.2,0 c0.7,0,1.4-0.1,2.1-0.1c0.7,0,1.4,0,2.1,0.1V15z M27.1,15"/></g></svg></a></li></ul></div>';
            $("#store-list").append(profile);
        }
        $("#store-info img").css("display", "none");
    });
}

function calculateLatLng(address) {
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            map.setCenter(results[0].geometry.location);

            marker = new google.maps.Marker({
                position: results[0].geometry.location,
                map: map
            });

            var longitude = results[0].geometry.location.lng();
            var latitude = results[0].geometry.location.lat();

            $("#hidden-latitude").val(latitude.toFixed(6));
            $("#hidden-longitude").val(longitude.toFixed(6));
        }
    });
}

function calculateLatLngCity(city) {
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({ 'address': city }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            var longitude = results[0].geometry.location.lng();
            var latitude = results[0].geometry.location.lat();

            $("#hidden-latitude").val(latitude.toFixed(6));
            $("#hidden-longitude").val(longitude.toFixed(6));
        }
    });
}