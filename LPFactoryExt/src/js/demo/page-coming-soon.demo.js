/*
Template Name: ASPSTUDIO - Responsive Bootstrap 4 Admin Template
Version: 1.1.0
Author: Sean Ngu
Website: http://www.seantheme.com/asp-studio/
*/

var handleRenderCountdownTimer = function() {
	var newYear = new Date();
	newYear = new Date(newYear.getFullYear() + 1, 1 - 1, 1);
	$('#timer').countdown({until: newYear});
};


/* Controller
------------------------------------------------ */
$(document).ready(function() {
	handleRenderCountdownTimer();
});