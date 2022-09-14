<template>
    <div class="d-flex justify-end">
        <v-btn
        @click="$emit('delete-photo')"
        class="mt-2 mr-2"
        color="error darken-1"
        small
        fab
        >
            <v-icon>{{icons.mdiDelete}}</v-icon>
        </v-btn>
        <v-btn
        @click="$emit('edit-photo')"
        class="mt-2 mr-2"
        color="success darken-1"
        small
        fab
        >
            <v-icon>{{icons.mdiFileDocumentEditOutline}}</v-icon>
        </v-btn>
    </div>
</template>
<script>
    import { mdiDelete } from '@mdi/js'
    import { mdiFileDocumentEditOutline } from '@mdi/js';
    
    export default {
        name: 'options-bar',
        data(){
            return {
                icons: {
                    mdiDelete,
                    mdiFileDocumentEditOutline
                }
            }
        },
    }
</script>