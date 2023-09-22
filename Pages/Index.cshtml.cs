@page
@model IndexModel
@{
    ViewData["Title"] = "Home Page";
}

<html>
<head>
    <meta charset="utf-8" />
    <title>jQuery Example</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
</head>
<div class="main-text-center">
            
    <ul>
            @for (int i = 0; i < Model.PokemonNames.Count; i++) 
        {
            <div class="pokemonCard">
            <img onmouseenter={pokeMouseEnter(event)} onmouseleave={pokeMouseLeave(event)} onclick={pokemonClick(event)} id="@i" src="@Model.PokemonSrc[i]" alt="Pokemon Image" />
            <li onclick={testclick()} id="@pokemon-text[i]" class="pokemon-text-container1"> @Model.PokemonNames[i] </li>            
            </div>
            @* <img onclick={pokemonClick(event)} id="@i" src="@Model.PokemonSrcBack[i]" alt="Pokemon Image" /> *@

        }
    </ul>
</div>

<script>
var pokemonSrcBack = @Html.Raw(Json.Serialize(Model.PokemonSrcBack));
var pokemonSrc = @Html.Raw(Json.Serialize(Model.PokemonSrc));

const pokemonIdFromSrc = (src) => {
        if (typeof src === "string") 
    {
        let srcId = src.replace(/\D+/g, '');
        return srcId
    } else
    {
        return;
    }
}

const pokeMouseEnter = (event) => {
    console.log('event', event);
    let src = event.target.src;
    let srcId = pokemonIdFromSrc(src);
    $(event.target).attr('src', pokemonSrcBack[srcId - 1]);
}

const pokeMouseLeave = (event) => {
    let src = event.target.src;
        if (typeof src === "string")
    {
        let srcId = pokemonIdFromSrc(src);
        $(event.target).attr('src', pokemonSrc[srcId - 1]);
    } else 
    {
        return;
    }
}

const pokemonClick = (event) => {
    console.log('event', event)
    let src = event.target.src
    let srcId = src.replace(/\D+/g, '');
    console.log(src);
    console.log(srcId);
    $('#pokemon-text[i]').textContent = "hey";
}

const testclick = () => {
    // Make a GET request to your API
    $.ajax({
        url: 'http://localhost:5278/api/pokemon', // Replace with the correct API endpoint 
        method: 'GET',
        success: function (data) {
            console.log("hey were in the successsss");
            // Handle the API response data here
            console.log('API response:', data);
        },
        error: function (error) {
            // Handle errors here
            console.log("big failure lets go!");
            console.error('API error:', error);
        }
    });
}

    @* async function fetchData() {
        try {
            const response = await fetch('/api/pokemon/test'); // Replace with the correct API endpoint
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const data = await response.json();
            console.log(data); // Handle the API response data here
        } catch (error) {
            console.error('API error:', error);
        }
    }

    // Call the fetchData function when the page loads
    window.addEventListener('load', fetchData); *@

</script>
