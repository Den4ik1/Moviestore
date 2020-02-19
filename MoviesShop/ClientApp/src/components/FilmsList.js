import '../app.css';
import React from 'react'

const FilmsList = (props) => {
    return (
        <table className='table'>
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Title</th>
                    <th>Year</th>
                    <th>Country</th>
                    <th>Actors</th>
                    <th>Genre</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
                {props.forecasts.map(forecast =>
                    <tr key={forecast.id}>
                        <td>{forecast.id}</td>
                        <td>{forecast.title}</td>
                        <td>{forecast.year}</td>
                        <td>{forecast.countryDTO.title}</td>
                        <td>{forecast.actorDTO.map(forc =>
                            <tr>{forc.title}</tr>
                        )}</td>
                        <td>{forecast.genreDTO.map(forc =>
                            <tr>{forc.title}</tr>
                        )}</td>
                        <td>
                            <button class="editButton" onClick={() => props.submitEdit(forecast)}>Edit</button>

                        </td>

                        <td>
                            <button class="editButton" onClick={() => props.submitDelete(forecast.id)}>Delete</button>
                        </td>
                    </tr>
                )}
            </tbody>
        </table >
    )
}
export default FilmsList;