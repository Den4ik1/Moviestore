import React from 'react';
import {Label } from 'reactstrap';
import '../app.css';

//////////////////////////////////////  1900/01/01
const saveSubmitEdit = (e) => {
        alert("Actor changed"),
        fetch(`api/Actors?Id=${encodeURIComponent(e.id)}`, {
        method: 'post',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            name: e.name,
            birthDay: e.birthDay,
            countryDTO: {
                title: e.countryDTO,
            },
            films: e.films.map(function (item) { return { title: item } })
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
                            <input type="text" name="birthDay" onChange={props.onChange} value={props.birthDay} placeholder="1990/12/31" /><p />

                            <Label for="country">Country:</Label>
                            <input type="text" name="countryDTO" onChange={props.onChange} value={props.countryDTO} /><p />
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                            <Label for="Films">Films:</Label>
                    <textarea name="newListFilm" onChange={props.onChangeArea} value={props.newListFilm.map(r => r).join("\n")} class="areaSize" />
                        </div>

                        <div class="boxForTaxtBox">
                            <h3> <b>Add films for actor</b></h3>
                            <select name="filmItem" size="6" onChange={e => props.onChange(e)} >
                        {props.fullListFilms.map(film => (
                            <option key={film.filmTitle} value={film.filmId}>
                                {film.filmTitle}
                                    </option>
                                ))}
                            </select>
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