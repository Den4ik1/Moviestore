import '../app.css';
import React from 'react';
import { Label } from 'reactstrap';

const saveSubmitEdit = (e) => {
    alert("Film changed"),
        fetch(`api/Film?Id=${encodeURIComponent(e.id)}`, {
            method: 'post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                title: e.title,
                year: e.year,
                countryDTO: {
                    title: e.countryDTO
                },
                genreDTO: e.genreDTO.map(function (itemGenre) { return { title: itemGenre } }),
                actorDTO: e.actorDTO.map(function (itemActor) { return { name: itemActor } }),
                urlImage: e.urlImage,
            }),
        })
    e.clearForm()
}


const FilmsForm = props => {
    return (
        <div className="content">
            <div class="boxforEditforms">
                <div class="boxForTaxtBox">
                    <h3> <b>Add film</b></h3>
                    {props.flag ? (
                        <div>
                            <Label for="id">Id:</Label><p />
                            <input type="text" name="id" value={props.id} />  <p />
                        </div>
                    ) : (
                            <div />
                        )}

                    <Label for="title">Film:</Label>
                    <input type="text" name="title" onChange={props.onChange} value={props.title} /><p />

                    <Label for="year">Year:</Label>
                    <input type="text" name="year" onChange={props.onChange} value={props.year} /><p />

                    <Label for="countryDTO">Country:</Label>
                    <input type="text" name="countryDTO" onChange={props.onChange} value={props.countryDTO} /><p />

                    <Label for="GenreDTO">Genre:</Label>
                    <textarea name="genreDTO" onChange={props.onChangeArea} value={props.genreDTO.map(r => r).join("\n")} class="areaSize" /><p />

                    <Label for="ActorDTO">Actors:</Label>
                    <textarea name="actorDTO" onChange={props.onChangeArea} value={props.actorDTO.map(r => r).join("\n")} class="areaSize" /><p />

                    <Label for="Image">Url Image:</Label>
                    <input type="urlImage" name="urlImage" onChange={props.onChange} value={props.urlImage} /><p />
                </div>

                <div class="boxForTaxtBox">
                    <h3> <b>Add genres for film</b></h3>
                    <select name="genreItem" size="6" onChange={e => props.onChange(e)} >
                        {props.genreList.map(genre => (
                            <option key={genre.title} value={genre.title}>
                                {genre.title}
                            </option>
                        ))}
                    </select>
                    <button
                        className="editButton"
                        onClick={() => props.addGenre(props.genreItem)}
                    >
                        Genre
                                    </button>

                    <h3> <b>Add actors for film</b></h3>
                    <select name="actorItem" size="6" onChange={e => props.onChange(e)} >
                        {props.actorList.map(actor => (
                            <option key={actor.title} value={actor.title}>
                                {actor.title}
                            </option>
                        ))}
                    </select>
                    <button
                        className="editButton"
                        onClick={() => props.addActor(props.actorItem)}
                    >
                        Actor
                                    </button>
                </div>
            </div>
            {
                props.flag ? (
                    <button class="editButton" onClick={() => saveSubmitEdit(props)}>Change</button>
                ) : (
                        <button class="editButton" onClick={() => saveSubmitEdit(props)}>Add</button>
                    )
            }

            {
                props.flag ? (
                    <button class="editButton" onClick={() => props.clearForm()}>New</button>
                ) : (
                        <div />
                    )
            }

        </div>
    )
}
export default FilmsForm