﻿import React from 'react';
import { FormGroup, Label } from 'reactstrap';
import '../../app.css';

const saveSubmitEdit = (e) => {
        alert("User changed"),
        fetch(`api/Users?id=${encodeURIComponent(e .id)}`, {
        method: 'post',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            name: e.name,
            age: e.age
        })
        }),
    e.clearForm
}

const UserForm = props => {
    return (
        <table>
            <tr>
                <td colspan="2">
                    <h3> <b>Register user</b></h3>
                    <FormGroup>
                        {props.flag ? (
                            <div>
                                <Label for="id">Id:</Label><p />
                                <input type="text" name="id" value={props.id} class="areaSize"/>  <p />
                            </div>
                        ) : (
                            <div />
                        )}
                        <Label for="name">Name:</Label>
                        <input type="text" name="name" onChange={props.onChange} value={props.name} class="areaSize"/><p />

                        <Label for="age">Age:</Label>
                        <input type="text" name="age" onChange={props.onChange} value={props.age} class="areaSize"/>
                    </FormGroup>
                </td>
            </tr>
            <tr>
                <td>
                    {
                        props.flag ? (
                            <button class="okButton" onClick={() => saveSubmitEdit(props)}>Change</button>
                        ) : (
                                <button class="okButton" onClick={() => saveSubmitEdit(props)}>Add</button>
                        )
                    }
                </td>
                <td>
                    {
                        props.flag ? (
                            <button class="noButton" onClick={() => props.clearForm()}>New</button>
                        ) : (
                            <div />
                        )
                    }
                </td>
            </tr>
        </table>
    )
}
export default UserForm;