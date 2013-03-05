﻿/// <reference path="../scripts/jquery-1.9.1.js" />
/// <reference path="../scripts/jquery.signalR-1.0.1.js" />
/// <reference path="CounterChart.js" />
$(function () {
    // Autogenerated client side hub
    var hub = $.connection.visualization,
        graphs = {},
        graphRoot = document.getElementById("charts"),
        startButton = $("#start"),
        endButton = $("#end");

    function init() {
        return hub.server.getCounters().done(function (all) {
            $.each(all, function () {
                graphs[this.Name] = new CounterChart(this, graphRoot);
            });
        });
    }

    // Add client-side hub methods that the server will call
    $.extend(hub.client, {
        counterValueChanged: function (counter) {
            graphs[counter.Name].update(counter.Value);
        }
    });

    // Start the connection
    $.connection.hub.start()
        .then(init)
        .done(function () {
            // Wire up the buttons
            startButton.click(function () {
                hub.server.start();
                
                // This does not take the actual server value into account
                endButton.removeAttr("disabled");
                startButton.attr("disabled", "disabled");
            });

            endButton.click(function () {
                hub.server.end();
                
                // This does not take the actual server value into account
                startButton.removeAttr("disabled");
                endButton.attr("disabled", "disabled");
            });
        });
});