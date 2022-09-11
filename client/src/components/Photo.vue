<template>
<v-col
    class="d-flex child-flex flex-column mt-3"
    cols="6"
>
    <div class="d-flex justify-space-between mb-2 text-subtitle-2">
        <div>
            <v-chip
            class="px-2"
            v-for="tag in photo.tags"
            :key="tag.id">
            {{tag.name}}
            </v-chip>
        </div>
        <div class="text-right">
            <v-chip outlined>{{photo.createdAt.substring(0,10)}}</v-chip>
        </div>
    </div>
    <v-img
    :src="$http.defaults.baseURL + photo.thumbnailUrl"
    lazy-src="@/assets/1.png"
    :aspect-ratio="16/9"
    class="grey lighten-2"
    >
        <div class="d-flex justify-end">
            <v-btn
            @click="deletePhoto(photo.id)"
            class="mt-2 mr-2"
            color="error darken-1"
            small
            fab
            :loading="isDeletingPhoto.includes(photo.id)"
            >
                <v-icon>{{icons.mdiDelete}}</v-icon>
        </v-btn>
        </div>
        <template v-slot:placeholder>
            <v-row
            class="fill-height ma-0"
            align="center"
            justify="center"
            >
            <v-progress-circular
                indeterminate
                color="grey lighten-5"
            ></v-progress-circular>
            </v-row>
        </template>
    </v-img>
    <div class="text-h5 mt-1 text-center">{{ photo.title }}</div>
    <div class="text-subtitle-1 text-center font-italic">{{photo.description  || "&nbsp;"}}</div>
</v-col>
</template>
<script>
    import {
    mdiDelete
  } from '@mdi/js'

    export default {
        name: 'photo-item',
        props: ['photo'],
        data(){
            return {
                icons: {
                    mdiDelete,
                },
                isDeletingPhoto: []
            }
        },
        methods:{
            async deletePhoto(id){
                this.isDeletingPhoto.push(id)

                const self = this
                
                const response = await this.$http.delete('photos/delete',id)
                    .catch(function (response) {
                        if(response && response.data)
                        {
                            var errorCode = response.data.errorCode
                            if(30 == errorCode) // photo not exists
                            {
                                self.$emit('remove-photo',id)
                            }
                        }
                    })

                if(response && response.data.status)
                {
                    alert('deleted')
                }
                
                this.isDeletingPhoto = this.isDeletingPhoto.filter(e => e != id)
            }
        }
    }
</script>