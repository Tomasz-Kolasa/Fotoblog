<template>
    <div class="d-flex justify-end">
        <v-btn
        @click="$emit('delete-photo')"
        class="mt-2 mr-2"
        color="error darken-1"
        small
        fab
        :loading="photosStates.beingDeleted.includes(photoId)"
        >
            <v-icon>{{icons.mdiDelete}}</v-icon>
        </v-btn>
    </div>
</template>
<script>
    import { mdiDelete } from '@mdi/js'
    
    export default {
        name: 'options-bar',
        props: ['photosStates', 'photoId'],
        data(){
            return {
                icons: {
                    mdiDelete,
                }
            }
        },
    }
</script>