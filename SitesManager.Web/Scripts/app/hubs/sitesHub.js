$(function() {
    var hub = $.connection.sitesHub;
    $.connection.hub.start();

    hub.client.sitesStatusReceived = function(obj) {
        var jsonResponse = $.parseJSON(obj);
        $.each(jsonResponse,
            function(index, value) {

                var siteTr = $("tr[data-id=" + value.Id + "]");
                if (siteTr.length > 0 && siteTr.data("status") !== value.Status) {
                    siteTr.data("status", value.Status);

                    var siteCells = siteTr.find("td");

                    var link = $(siteCells[0]).find("a");
                    link.text(value.Name);
                    link.attr("href", value.Url);
                    $(siteCells[1]).html((value.Description != null ? value.Description.replace(/\n/g, "<br />") : ""));
                    $(siteCells[2]).html(value.StatusName + " (" + value.Status + ")");

                    siteTr.effect("highlight", 1000);
                }
            });
    };
});