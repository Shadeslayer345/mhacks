/** Meant to be used in conjunction with the Unity game in the repo
        (which we couldn't get to play nice and overlay on a page). Not
        extensively tested but produced nice results at
        https://www.dhs.state.il.us/accessibility/tests/flash/video.html. */


var head = document.querySelector('head');
var flashContent = document.querySelector('[data]');

/**
 * Throwing the commercial riddled content to the side so the real star can
 *         shine.
 */
var addResize = function() {
    var css = '#resize { left: 0; position: absolute; width: 50px; }';
    var style = document.createElement('style');

    if (style.styleSheet){
        style.styleSheet.css = css;
    } else {
        style.appendChild(document.createTextNode(css));
    }

    head.appendChild(style);

    flashContent = document.querySelector('[data]');
    flashContent.id = "resize";
}

/**
 * Get rid of our lovely work!
 */
var removeResize = function() {
    flashContent.id = "";
}
