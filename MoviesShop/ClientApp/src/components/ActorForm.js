import React from 'react';
import { Label } from 'reactstrap';
import '../app.css';

//////////////////////////////////////
const saveSubmitEdit = (e) => {
    {
        e.newListFilms.map(x => (
            console.log(x.filmId)));
    };

    alert("Actor changed"),
        fetch(`api/Actors?id=${encodeURIComponent(e.id)}`, {
            method: 'post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                name: e.name,
                birthDay: e.birthDay,
                countryDTO: {
                    countryTitle: e.countryDTO,
                },
                filmsActorDTO: e.newListFilms.map(function (x) { return { secondId: x.filmId } }),
            })
        })
    e.clearForm()
}

const ActorForm = props => {
    return (
        <div className="content">
            <div class="boxforEditforms">
                <div class="boxForTaxtBox">
                    <h3> <b>Add actor</b></h3>
                    {props.flag ? (
                        <div>
                            <Label for="id">Id:</Label><p />
                            <input type="text" name="id" value={props.id} />  <p />
                        </div>
                    ) : (
                            <div />
                        )}

                    <Label for="name">Name:</Label>
                    <input type="text" name="name" onChange={props.onChange} value={props.name} /><p />

                    <Label for="birthDay">Birth day:</Label>
                    <input type="text" name="birthDay" onChange={props.onChange} value={props.birthDay.slice(0, 10)} placeholder="1990-12-31" /><p />

                    <Label for="countryDTO">Country:</Label>
                    <input type="text" name="countryDTO" onChange={props.onChange} value={props.countryDTO} /><p />

                    <Label for="filmsDTO">Films:</Label>
                    <textarea name="newListFilm" onChange={props.onChangeArea} value={props.filmsDTO.map(r => r).join("\n")} class="areaSize" />
                </div>

                <div class="boxForTaxtBox">

                    <h3> <b>Add films for actor</b></h3>
                    <div>
                        <select name="filmItem" size="6" onChange={props.onChange} >
                        {props.fullListFilms.map(film => (
                            <option key={film.title} value={film.id}>
                                "{film.title}" - {film.countryDTO.countryTitle} - {film.year}
                            </option>
                        ))}
                    </select>
                        </div>

                    <button
                        className="editButton"
                        onClick={() => props.addFilm(props.filmItem)}
                    >
                        Film
                            </button>
                </div>


            </div>
            {
                props.flag ? (
                    <button class="okButton" onClick={() => saveSubmitEdit(props)}>Change</button>
                ) : (
                        <button class="okButton" onClick={() => saveSubmitEdit(props)}>Add</button>
                    )
            }

            {
                props.flag ? (
                    <button class="noButton" onClick={() => props.clearForm()}>New</button>
                ) : (
                        <div />
                    )
            }
        </div>
    )
}
export default ActorForm;