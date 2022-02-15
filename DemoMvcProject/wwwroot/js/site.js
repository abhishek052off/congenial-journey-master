// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function index_get_helper(urlString,operation){
    console.log(operation+' -@- ' + new Date().toLocaleString());
    var form = $('#ajax-form-result')
    .removeData("validator") 
    .removeData("unobtrusiveValidation");
    
    $.get(urlString).
            done(function(data){
                $(form).html(data);
               
                $.validator.unobtrusive.parse(form);
                $('#ajax-popup-modal').modal('show');
                $('#ajax-modal-title').html(operation)
               
               
            }).fail(function(response){
                $(form).html('<p>Something Went Wrong , Please try again </p>');
                $('#ajax-popup-modal').modal('show');
                $('#ajax-modal-title').html(operation)
            });
       
    
    
}


function clear_fields(){
    $(':input','#search-form')
    .not(':button, :submit, :reset, :hidden')
    .val('')
    
}

$(document).ready( function () {
    $('#result_grid').DataTable();
} );
                                             
