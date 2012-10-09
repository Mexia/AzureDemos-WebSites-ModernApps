/// <reference path="../knockout-2.0.0.debug.js" />
/// <reference path="../jquery-ui-1.8.11.js" />
/// <reference path="../jquery-1.7.2.js" />
/// <reference path="../ajax-util.js" />
/// <reference path="../ko-protected-observable.js" />
$(function () {
    var hub = $.connection.chirpyRHub,
    $msgs = $("#chirpStream");
    hub.NewChirp = function (chirp) {

        addChirp(chirp);
    }

    function addChirp(chirp) {
        viewModel.chirps.unshift(new chirpItem(chirp.Text,
                          chirp.Id, chirp.ChirpBy.Gravataar,
                          chirp.ChirpBy.UserId));
    }

    $.connection.hub.start();
    var data = [
        new chirpItem(
            "Checkout my Elements of Distributed Architecture course on Pluralsight",
            1,
            'http://www.gravatar.com/avatar/397b74a72dd89612c3c56edf9e75cc6d?s=48&d=identicon&r=PG',
            'Clemens Vasters'),
        new chirpItem(
            "SignalR is crazy cool",
            2,
            "http://www.gravatar.com/avatar/719c91f5c3013e43ee46ed2bdc67f883?s=48&d=identicon&r=PG",
            'Scott Hanselman'),
        new chirpItem("KnockoutJS FTW",
            3,
            "http://www.gravatar.com/avatar/6d8ebb117e8d83d74ea95fbdd0f87e13?s=48&d=identicon&r=PG",
            'Jon Skeet'),
    ];

    function chirpItem(text, id, gravatar, by) {
        return {
            Text: text,
            Id: id,
            GravatarUrl: gravatar,
            By: by
        };
    }

    var viewModel = {
        // data
        chirps: ko.observableArray(data),
        currentUser: ko.observable({ UserName: "unknown", OldPassword: "" })
    }
    ko.applyBindings(viewModel);

    $.getJSON("Api/ChirpyR", null, function (data) {
        for (var i in data) {
            addChirp(i);
        }
    });

    $(document).on("click",
        "#postChirp", function () {
            var chirp = {
                "Text": $("#chirpText").val()
            };
            ajaxAdd("/Api/ChirpyR",
                ko.toJSON(chirp), function (data) {
                });
            $("#chirpText").val("");
        });
});