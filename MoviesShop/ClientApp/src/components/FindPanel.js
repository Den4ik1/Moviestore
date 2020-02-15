import React from 'react';
import {  Label } from 'reactstrap';



const FindPanel = props => {
    return (
        <table>
            <tr>
                <td>
                    <div class="boxEditButton">
                        <Label for="find">Find To Id</Label> <p />
                        <input type="text" name="indexFinde" onChange={props.onChange} value={props.indexFinde} /> <p />
                        <button class="editButton" onClick={() => props.submitFind(props.indexFinde)}>Find</button>
                    </div>
                </td>
                <td>
                   <div class="boxEditButton">
                       <Label for="data">Find</Label> <p />
                        <input type="text" name="indexTitle" onChange={props.onChange} value={props.indexTitle} /> <p />
                        <button class="editButton" onClick={() => props.submitFindeTitle(props.indexTitle)}>Find</button>
                   </div>
                </td>
                <td>
                   <div class="boxEditButton">
                       <Label for="delete">Delete To Id</Label> <p />
                       <input type="text" name="indexDelete" onChange={props.onChange} value={props.indexDelete} /> <p />
                        <button class="editButton" onClick={() => props.submitDelete(props.indexDelete)} >Delete</button>
                   </div>
                </td>
            </tr>
        </table>
        )
}
export default FindPanel


