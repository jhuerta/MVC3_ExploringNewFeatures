var learnMVC3 = {
    resetResult: function () {
        $(".search-results").empty();
    },
    getResult: function (value, controllerName) {
        $.getJSON(controllerName, { query: value }, function (data) {
            var results = { controller: controllerName, items: data };
            $("#searchTemplate").tmpl(results).appendTo("#" + controllerName + "-search-results");
        });
    }
};


jQuery(function () {
    $("#searchForm").submit(function () {
        var queryValue = $("#search").val();
        if (queryValue.length > 0) {

            learnMVC3.resetResult();
            learnMVC3.getResult(queryValue, "productionsv2");
            learnMVC3.getResult(queryValue, "episodesv2");
            learnMVC3.getResult(queryValue, "customersv2");
        }
        else {

            $(".search-results").first().html(" Need to input value");
        }
        return false;
    });
});

