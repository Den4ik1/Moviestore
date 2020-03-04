import '../app.css';
import React from 'react'

const ActorList = (props) => {
    return (
        <table className='table'>
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Name</th>
                    <th>BirthDay</th>
                    <th>Country</th>
                    <th>Films</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
                {props.forecasts.map(forecast =>
                    <tr key={forecast.id}>
                        <td>{forecast.id}</td>
                        <td>{forecast.name}</td>
                        <td>{forecast.birthDay.slice(0, 10)}</td>
                        <td>{forecast.countryDTO.title}</td>
                        <td>{forecast.filmsDTO.map(forc =>
                            <tr>{forc.title}</tr>
                        )}</td>
                        <td>
                            <button class="editButton" onClick={() => props.submitEdit(forecast)}>Edit</button>

                        </td>

                        <td>
                            <button class="noButton" onClick={() => props.submitDelete(forecast.id)}>Delete</button>
                        </td>
                    </tr>
                )}
            </tbody>
        </table>
    )
}

export default ActorList;