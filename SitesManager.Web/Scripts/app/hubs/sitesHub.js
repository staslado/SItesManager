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

                    $(siteCells[0]).find("a").text(value.Name);
                    $(siteCells[1]).html((value.Description != null ? value.Description.replace(/\n/g, "<br />") : ""));

                    if (value.Status === 0) {
                        siteTr.find("td[name=activeStatus]").hide();
                        siteTr.find("td[name=disableStatus]").show();
                    } else {
                        siteTr.find("td[name=disableStatus]").hide();
                        siteTr.find("td[name=activeStatus]").show();
                    }

                    siteTr.effect("highlight", 1000);
                }
            });
    };
});