import React from 'react'
import '../app.css'

export default class Header extends React.Component {
    render() {
        return (
            <header>
                <table>
                    <tr>
                        <th>
                            <form action='/Films' method="get">
                                <input className="headButton" type="submit" value="Films" />
                            </form>
                        </th>
                        <th>
                            <form action='/Actors' method="get">
                                <input className="headButton" type="submit" value="Actors" />
                            </form>
                        </th>
                        <th>
                            <form action='/Users' method="get">
                                <input className="headButton" type="submit"  value="Users" />
                            </form>
                        </th>
                        <th>
                            <form action='/FilmsEdit' method="get">
                                <input className="headButton" type="submit" value="FilmsEdit" />
                            </form>
                        </th>
                        <th>
                            <form action='/ActorEdit' method="get">
                                <input className="headButton" type="submit" value="ActorEdit" />
                            </form>
                        </th>
                        <th>
                            <form action='/UserEdit' method="get">
                                <input className="headButton" type="submit" value="UserEdit" />
                            </form>
                        </th>
                    </tr>
                  </table>
            </header>
        )
    }
}