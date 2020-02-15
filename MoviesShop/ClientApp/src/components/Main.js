import React from 'react'
import { Switch, Route } from 'react-router-dom'
import { Films } from './Films'
import { Actors }  from './Actors'
import { Users } from './Users'
import { FilmsEdit } from './FilmsEdit'
import { ActorEdit } from './ActorEdit'
import { UserEdit } from './UserEdit'
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