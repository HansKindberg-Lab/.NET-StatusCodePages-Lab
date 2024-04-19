import $ from "jquery";

console.log("Module-1.ts");

$(() => {
	$("#click-me-button").on("click", () => {
		alert("Yeah (alert added in script with jQuery)");
	});
});