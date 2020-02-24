import '../app.css';
import React from 'react'

const UserList = (props) =>
{
    return (
        <table className='table'>
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Name</th>
                    <th>Age</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
                {props.forecasts.map(forecast =>
                    <tr key={forecast.id}>
                        <td> {forecast.id} </td>
                        <td> {forecast.name}</td>
                        <td> {forecast.age}</td>
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

export default UserList;