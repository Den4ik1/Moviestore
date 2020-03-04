import React from 'react'
import { Switch, Route } from 'react-router-dom'
import { Films } from './Film/Films'
import { Actors } from './Actor/Actors'
import { Users } from './User/Users'
import { FilmsEdit } from './Film/FilmsEdit'
import { ActorEdit } from './Actor/ActorEdit'
import { UserEdit } from './User/UserEdit'
import '../app.css'

 class Main extends React.Component {
    render() {
        return (
            <div>
            <main >
                <Switch>
                    <Route exact path='/Films' component={Films} />
                    <Route path='/Actors' component={Actors} />
                    <Route path='/Users' component={Users} />
                    <Route path='/ActorEdit' component={ActorEdit} />
                    <Route path='/FilmsEdit' component={FilmsEdit} />
                    <Route path='/UserEdit' component={UserEdit} />
                </Switch>
                </main>

                <div />
            </div >
        )
    }
}

export default Main